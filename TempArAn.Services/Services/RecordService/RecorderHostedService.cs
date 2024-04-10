using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Models.Record;

namespace TempArAn.Services.Services.RecordService
{
    public class RecorderHostedService : IRecorderHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer? _timer = null;
        private readonly ILogger<RecorderHostedService> _logger;
        private readonly int _interval; // mseconds
        private readonly int _countRecordThreads;

        public RecorderHostedService(IServiceScopeFactory scopeFactory, ILogger<RecorderHostedService> logger, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _interval = int.Parse(configuration["RecordingInterval"] ??
                throw new NullReferenceException());
            _countRecordThreads = int.Parse(configuration["RecordingThreads"] ??
                throw new NullReferenceException());
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(InitRecordingAsync, null, 0, _interval);
            return Task.CompletedTask;
        }

        public async void InitRecordingAsync(object? state)
        {
            _logger.Log(LogLevel.Information, "start colleting");
            using (var scope = _scopeFactory.CreateScope())
            {
                var rep = scope.ServiceProvider.GetRequiredService<IApplicationUnitOfWork>();
                var sources = await rep.Sources.GetSourcesForRecordAsync();
                var records = new List<DoubleRecord>();
                var errors = new List<SourceErrorRecord>();

                await RecordSourcesListAsync(sources.ToList(), records, errors, _logger, _countRecordThreads);

                await rep.ErrorRecords.PostRecordsAsync(errors);
                await rep.DoubleRecords.PostRecordsAsync(records);
                await rep.Sources.PutSoursesAsync(sources);

                _logger.Log(LogLevel.Information, $"collected {sources.Count()}  sources");
            }
        }

        public static async Task RecordSourcesListAsync(
            List<SourceBase> sources,
            List<DoubleRecord> records,
            List<SourceErrorRecord> errors,
            ILogger<RecorderHostedService> logger,
            int countRecordThreads)
        {
            var semafore = new SemaphoreSlim(countRecordThreads);
            var results = new RecordBase[sources.Count];
            var tasks = new Task[sources.Count];

            for (var count = 0; count < sources.Count; count++)
                tasks[count] = Task.Run(RecordFromSource(sources, logger, semafore, results, count));
            await Task.WhenAll(tasks);

            foreach (var result in results)
            {
                if (result is SourceErrorRecord sourceError)
                    errors.Add(sourceError);
                else
                    records.Add(result as DoubleRecord ??
                        throw new ArgumentException("There is not a Double Record"));
            }
        }

        private static Func<Task> RecordFromSource(List<SourceBase> sources, ILogger<RecorderHostedService> logger, SemaphoreSlim semafore, RecordBase[] results, int safeCount)
        {
            return async () =>
            {
                await semafore.WaitAsync();
                try
                {
                    results[safeCount] = sources[safeCount].TryRecording();
                }
                catch (Exception ex)
                {
                    logger?.Log(LogLevel.Error, ex.Message);
                }
                semafore.Release();
            };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
