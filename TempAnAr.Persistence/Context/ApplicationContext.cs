using Microsoft.EntityFrameworkCore;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Models.Source;
using TempArAn.Domain.Models.User;

namespace TempAnAr.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DoubleRecord> DoubleRecords { get; set; } = null!;
        public DbSet<TemperatureDataSetRecords> DataRecords { get; set; } = null!;
        public DbSet<SourceErrorRecord> ErrorRecords { get; set; } = null!;
        public DbSet<HTMLSource> Sources { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<DoubleRecord>().HasKey(x => new { x.SourceId, x.DateTime });
            mb.Entity<TemperatureDataSetRecords>().HasKey(x => new { x.SourceId, x.DateTime });
            mb.Entity<SourceErrorRecord>().HasKey(x => new { x.SourceId, x.DateTime });
            base.OnModelCreating(mb);
        }
    }
}
