using AutoMapper;
using TempAnAr.Persistence.Context;
using TempArAn.Services.Mapper;

namespace TempArAn.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ApplicationContext context;
        public IMapper mapper;

        public QueryTestFixture()
        {
            context = ApplicationContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(RecordMappingProfile).Assembly);
            });
            mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(context);
        }
    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
