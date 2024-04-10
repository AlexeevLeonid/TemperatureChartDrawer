using TempArAn.Domain.AbstractCore;
using TempArAn.Services.Services.RecordService;
using TempArAn.Tests.Common;

namespace TempArAn.Tests.Tests.Perfomance.Recorder
{
    [Collection("QueryCollection")]
    public class SourceRecorderTest : BaseTest
    {
        public SourceRecorderTest(QueryTestFixture fixture) : base(fixture) { }

        [Fact]
        public async Task RecorderPerfomanceTest()
        {
            var mockSources = new List<MockSource>();
            for (var i = 0; i < 5; i++) mockSources.Add(new MockSource("a", 5, ApplicationContextFactory.UserA.Id));
            var results = new List<Domain.Models.Record.DoubleRecord>();
#pragma warning disable CS8625 // тесту не нужен корректно работающий логгер
            await RecorderHostedService.RecordSourcesListAsync(
                mockSources.Select(x => x as SourceBase).ToList(),
                results,
                new List<Domain.Models.Record.SourceErrorRecord>(),
                null,
                3);
#pragma warning restore CS8625 
            Assert.Equal(5, results.Count);
            Assert.True(true);
        }
    }
}
