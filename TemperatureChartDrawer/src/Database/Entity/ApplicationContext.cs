using Microsoft.EntityFrameworkCore;
using TemperatureChartDrawer.src.Recorder;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.src.Database.Entity
{
    public class ApplicationContext : DbContext
    {
        private IServiceScopeFactory Services { get; }
        public DbSet<Record> Records { get; set; } = null!;
        public DbSet<HTMLSource> Sources { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }
    }
}
