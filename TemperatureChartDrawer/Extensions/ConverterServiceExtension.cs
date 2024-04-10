using TempArAn.Services.Services.ConvertService;

namespace TempAnAr.Extensions
{
    public static class ConverterServiceExtension
    {
        public static void AddApplicationConverterService(this IServiceCollection services)
        {
            services.AddHostedService<RecordConverterHostedService>();
        }
    }
}
