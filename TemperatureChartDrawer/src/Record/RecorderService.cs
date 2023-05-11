using System.Collections.Generic;
using TemperatureChartDrawer.src.Database.Entity;
using TemperatureChartDrawer.src.Database.Interfaces;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.src.Recorder
{
    public class RecorderService<TSource> : IHostedService
        where TSource : SourceBase
    {
        private readonly IRepository<TSource> _rep;
        ILogger<RecorderService<TSource>> _logger;
        int _interval = 10000; // mseconds

        public async Task CollectingRecords(List<TSource> allSources)
        {
            var workedSources = new List<TSource>();
            foreach (var src in allSources)
            {
                try
                {
                    var result = src.TryRecording();
                    if (result != null)
                    {
                        await _rep.PostRecord(new Record(src.Id, result, DateTime.Now));
                        workedSources.Add(src);
                    }
                } catch (Exception ex) 
                {
                    _logger.Log(LogLevel.Error, ex.Message);
                }
            }
            await _rep.PutSourses(workedSources);
            _logger.Log(LogLevel.Information, $"collected {workedSources.Count} / {allSources.Count} sources");
        }

        public async Task LoopedRecording(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.Log(LogLevel.Information, "start colleting");
                try {
                    await CollectingRecords(await _rep.GetSourses());
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex.Message);
                }
                await Task.Delay(_interval, cancellationToken);
            }


        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            LoopedRecording(cancellationToken); //the application does not work if use await
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public RecorderService(IRepository<TSource> rep, ILogger<RecorderService<TSource>> logger, IConfiguration configuration)
        {
            _rep = rep;
            _interval = int.Parse(configuration["RecordingInterval"]);
            _logger = logger;
        }
    }
}
