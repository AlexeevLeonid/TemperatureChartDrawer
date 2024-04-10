using Microsoft.EntityFrameworkCore;
using TempAnAr.Persistence.Context;
using TempArAn.Domain.Enums;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Models.Source;
using TempArAn.Domain.Models.User;

namespace TempArAn.Tests.Common
{
    internal class ApplicationContextFactory
    {
#pragma warning disable CS8618 // поля не нужны без вызова create
        public static User UserA;
        public static User UserB;
        public static HTMLSource Source;
#pragma warning restore CS8618
        public static ApplicationContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationContext(options);
            UserA ??= new User("a", "a");
            context.Users.Add(UserA);
            Source ??= new HTMLSource("a", "a", "a", "a", UserA.Id, 5);
            context.Sources.Add(Source);

            var newRecords = new List<DoubleRecord>()
            {
                new DoubleRecord(Source.Id, DateTime.Now, 1.0),
                new DoubleRecord(Source.Id, DateTime.Now.Subtract(TimeSpan.FromMinutes(1)), 2.0),
                new DoubleRecord(Source.Id, DateTime.Now.Subtract(TimeSpan.FromMinutes(2)), 3.0),
            };

            var errorRecocrds = new List<SourceErrorRecord>()
            {
                new SourceErrorRecord(Source.Id, DateTime.Now, TypeSourceError.NotFound),
                new SourceErrorRecord(Source.Id, DateTime.Now.Subtract(TimeSpan.FromMinutes(1)), TypeSourceError.ParseError),
            };

            DateTime WeekAgo = DateTime.Now.Subtract(TimeSpan.FromDays(8));
            var oldRecords = new List<DoubleRecord>()
            {
                new DoubleRecord(Source.Id, WeekAgo, 1.0),
                new DoubleRecord(Source.Id, WeekAgo.Subtract(TimeSpan.FromMinutes(1)), 2.0),
                new DoubleRecord(Source.Id, WeekAgo.Subtract(TimeSpan.FromMinutes(2)), 3.0),
            };

            context.Database.EnsureCreated();

            context.ErrorRecords.AddRange(errorRecocrds);
            context.DoubleRecords.AddRange(oldRecords);
            context.DoubleRecords.AddRange(newRecords);
            context.SaveChanges();
            return context;
        }

        public static void Destroy(DbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
