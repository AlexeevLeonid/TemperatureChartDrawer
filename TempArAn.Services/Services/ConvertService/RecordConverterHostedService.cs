using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Models.Record;

namespace TempArAn.Services.Services.ConvertService
{
    public class RecordConverterHostedService : IRecordConverterHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer? _timer;
        private readonly ILogger<RecordConverterHostedService> _logger;

        public RecordConverterHostedService(
            IServiceScopeFactory serviceScopeFactory, ILogger<RecordConverterHostedService> logger)
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            InitConvertingAsync(null);
            _timer = new Timer(
                InitConvertingAsync,
                null,
                GetMillisecondsUntilNextMonday(),
                (int)TimeSpan.FromDays(7).TotalMilliseconds);
            return Task.CompletedTask;
        }


        public async void InitConvertingAsync(object? state)
        {
            _logger.Log(LogLevel.Information, "start convert records");
            using (var scope = _scopeFactory.CreateScope())
            {
                var rep = scope.ServiceProvider.GetRequiredService<IApplicationUnitOfWork>();
                var records = await rep.DoubleRecords.GetRecordsForTimeAsync(
                    DateTime.MinValue, DateTime.Now.AddDays(-7).Date);
                var dataRecords = new Stack<TemperatureDataSetRecords>();

                ConvertingAsync(records, dataRecords);

                await rep.DataRecords.PostRecordsAsync(dataRecords);
                await rep.DoubleRecords.DeleteRecordsAsync(records);
            }
        }


        public static void ConvertingAsync(IEnumerable<DoubleRecord> records, Stack<TemperatureDataSetRecords> dataRecords)
        {
            foreach (var recordToConvert in records.GroupBy(x => (x.SourceId, x.DateTime.Date)))
                dataRecords.Push(
                    GetDataFromRecords(recordToConvert, recordToConvert.Key.Date));

        }


        private static TemperatureDataSetRecords GetDataFromRecords(IEnumerable<DoubleRecord> input, DateTime date)
        {
            var records = input.
                Select(x => x.Value).
                OrderBy(x => x).ToList();

            var min = records[0];
            var max = records[^1];
            var mean = records.Average();
            double median;
            if (records.Count == 1) median = records[0];
            else if (records.Count == 2) median = mean;
            else median = records.Count % 2 != 0 ?
                records.OrderBy(x => x).ToList()[records.Count / 2] :
                (records[records.Count / 2] + records[records.Count / 2 + 1]) / 2;
            return new TemperatureDataSetRecords(input.First().SourceId, date, min, max, mean, median);
        }

        public static int GetMillisecondsUntilNextMonday()
        {
            var now = DateTime.Now;
            var nextMonday = now.AddDays((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7);
            var nextMondayMidnight = nextMonday.Date;
            var timeUntilNextMonday = nextMondayMidnight - now;
            return (int)timeUntilNextMonday.TotalMilliseconds;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
