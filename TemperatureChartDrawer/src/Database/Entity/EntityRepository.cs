using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;
using TemperatureChartDrawer.src.Database.Interfaces;
using TemperatureChartDrawer.src.Recorder;
using TemperatureChartDrawer.src.Sourse;

namespace TemperatureChartDrawer.src.Database.Entity
{
    public class EntityRepository :  IRepository<HTMLSource>
    {
        private readonly IServiceScopeFactory _factory;

        public async Task<List<ShortRecord>> GetRecordsFromSourse(Guid guid)
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                return context.Records.AsNoTracking()
                    .Where(x => x.SourceId == guid).Select(x => x.ToShortRecord()).ToList();
            };
        }

        public async Task<HTMLSource> GetSourse(Guid guid)
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                return await context.Sources.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == guid);
            };
        }

        public async Task PostRecord(Record record)
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                context.Records.Add(record);
                await context.SaveChangesAsync();
            };
        }


        public async Task PostSourse(HTMLSource source)
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                context.Sources.Add(source);
                await context.SaveChangesAsync();
            };
        }

        public async Task<List<HTMLSource>> GetSourses()
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                return context.Sources.ToList();
            };
        }

        public async Task DeleteSourse(Guid guid)
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                context.Sources.Remove(await context.Sources.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == guid));
                context.SaveChanges();
            };
        }

        public async Task PutSourses(List<HTMLSource> sources)
        {
            using (var context = _factory.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                foreach (var source in sources)
                {
                    var sourceInBase = context.Sources.AsNoTracking()
                        .FirstOrDefault(x => x.Id == source.Id);
                    sourceInBase = source;
                    context.Sources.Update(sourceInBase);
                }
                context.SaveChanges();
            };
        }

        public EntityRepository(IServiceScopeFactory factory)
        {
            _factory = factory;
        }
    }
}
