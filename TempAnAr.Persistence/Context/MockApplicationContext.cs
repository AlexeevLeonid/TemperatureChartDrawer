using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Linq;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Models.Source;
using TempArAn.Domain.Models.User;

namespace TempAnAr.Persistence.Context
{
    public class MockApplicationContext : ApplicationContext
    {
        public MockApplicationContext(DbContextOptions<MockApplicationContext> options) :
            base(new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ReplaceService<ApplicationContext, MockApplicationContext>()
                .Options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            var user = new User("a", "a");
            mb.Entity<User>().HasData(user);
            var sources = new List<HTMLSource>
            {
                new HTMLSource("теплица", "a", "a", "a", user.Id, 5),
                new HTMLSource("теплица", "a", "a", "a", user.Id, 5)
            };
            mb.Entity<HTMLSource>().HasData(sources);

            var records = new List<DoubleRecord>();
            var errorRecords = new List<SourceErrorRecord>();
            var dataRecords = new List<TemperatureDataSetRecords>();

            foreach (var source in sources) {
                var time = DateTime.Now;
                var random = new Random();
                var allRecords = new List<DoubleRecord>();
                var thisDay = (new DateTime(), 0.0);
                for (var dtime = DateTime.Now.AddMonths(-15); dtime < DateTime.Now; dtime = dtime.AddHours(1))
                {
                    if (dtime.Date != thisDay.Item1.Date)
                        thisDay = (dtime.Date, random.NextDouble() * 5 / 2);
                    allRecords.Add(new DoubleRecord(
                        source.Id,
                        dtime,
                        Math.Sin((double)(dtime.DayOfYear) / 365 * 2 * Math.PI) * 20 + 
                        Math.Cos((double)(dtime.Hour) / 24 * 2 * Math.PI) * 5 + thisDay.Item2));
                }
                var recordsToConvert = allRecords.Where(x=> x.DateTime < DateTime.Now.AddDays(-7).Date);
                foreach (var recordToConvert in recordsToConvert.GroupBy(x => (x.SourceId, x.DateTime.Date)))
                    dataRecords.Add(
                        new TemperatureDataSetRecords(
                            recordToConvert.Key.SourceId,
                            recordToConvert.Key.Date,
                            GetDataFromRecords(recordToConvert)));
                records.AddRange(allRecords.Where(x => x.DateTime > DateTime.Now.AddDays(-7)));
                var count = 0;
                for (var dtime = DateTime.Now.AddDays(-6); dtime < DateTime.Now; dtime = dtime.AddHours(3))
                {
                    errorRecords.Add(new SourceErrorRecord(source.Id, dtime.AddHours(-2), count % 2));
                    count = +1;
                }
            }


            mb.Entity<DoubleRecord>().HasData(records);
            mb.Entity<TemperatureDataSetRecords>().HasData(dataRecords);
            mb.Entity<SourceErrorRecord>().HasData(errorRecords);

            base.OnModelCreating(mb);
        }

        private static TemperatureDataSet GetDataFromRecords(IEnumerable<RecordBase> input)
        {
            var records = input.
                Select(x => (x as DoubleRecord ??
                throw new ArgumentException("There is not a String Record")).Value).
                OrderBy(x => x).ToList();
            var min = records[0];
            var max = records[records.Count - 1];
            var mean = records.Average();
            var median = records.Count() % 2 != 0 ?
                records.OrderBy(x => x).ToList()[records.Count() / 2] :
                (records[records.Count() / 2] + records[records.Count() / 2 - 1]) / 2;
            return new TemperatureDataSet(median, mean, min, max);
        }
    }
}
