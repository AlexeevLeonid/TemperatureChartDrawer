using TempArAn.Services.Mapper;

namespace TempAnAr.Extensions
{
    public static class ApplicationMapperExtension
    {
        public static void AddApplicationMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserMappingProfile),
                typeof(RecordMappingProfile),
                typeof(SourceMappingProfile)
                );
        }
    }
}
