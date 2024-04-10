using TempArAn.Domain.Models.Record;
using TempArAn.Services.Services.ConvertService;
using TempArAn.Tests.Common;

namespace TempArAn.Tests.Tests
{
    [Collection("QueryCollection")]
    public class ConverterTests : BaseTest
    {
        public ConverterTests(QueryTestFixture fixture) : base(fixture) { }

        [Fact]
        public async Task ConverterTest_Success()
        {

            var dataRecords = new Stack<TemperatureDataSetRecords>();
            var records = context.DoubleRecords.ToList();
            RecordConverterHostedService.ConvertingAsync(records, dataRecords);
            Assert.DoesNotContain(false, dataRecords.Select(drecord => 
                drecord.Mean == 2 &&
                drecord.Median == 2 &&
                drecord.Min == 1 &&
                drecord.Max == 3));
        }
    }
}
