using TempArAn.Services.Services.RecordService;

namespace TempAnAr.Extensions
{
    public static class RecorderServiceExtension
    {
        public static void AddApplicationRecorderService(this IServiceCollection services)
        {
            services.AddHostedService<RecorderHostedService>();
        }
    }
}
