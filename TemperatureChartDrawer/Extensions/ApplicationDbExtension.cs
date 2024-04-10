using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Implementations;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.Models.Record;

namespace TempAnAr.Extensions
{
    public static class ApplicationDbExtension
    {
        public static void AddApplicationDb(this IServiceCollection services, string? connection)
        {

            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<MockApplicationContext>(
                options => options.
                UseInMemoryDatabase(Guid.NewGuid().ToString()));

            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IRecordRepository<DoubleRecord>, DoubleRecordRepository>();
            //services.AddTransient<IRecordRepository<DataForDayRecord>, DataRecordRepository>();
            //services.AddTransient<IRecordRepository<SourceErrorRecord>, SourceErrorRecordRepository>();
            //services.AddTransient<ISourceRepository, HTMLSourceRepository>();

            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
        }
    }
}
