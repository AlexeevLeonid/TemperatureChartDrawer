using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempAnAr.Persistence.Implementations;
using TempArAn.Application.Records.Handlers;
using TempArAn.Application.Records.Requests;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Responses;
using TempArAn.Tests.Common;

namespace TempArAn.Tests.Tests
{
    [Collection("QueryCollection")]
    public class MapperTests : BaseTest
    {
        public MapperTests(QueryTestFixture fixture) : base(fixture) { }

        [Fact]
        public async Task MappingDoubleRecordTest()
        {
            var record = new DoubleRecord(new Guid(), DateTime.Now, 1.6);

            var result = mapper.Map<DoubleRecord, SimpleRecordResponse>(record);

            var datarecord = new TemperatureDataSetRecords(new Guid(), DateTime.Now, 1.5, 1.5, 1.5, 1.5);

            var result2 = mapper.Map<ComplexRecordResponse>(datarecord);

            var errorrecord = new SourceErrorRecord(new Guid(), DateTime.Now, 0);

            var result3 = mapper.Map<SimpleRecordResponse>(errorrecord);

            Assert.Equal(
                record.Value.ToString(CultureInfo.InvariantCulture), result.Value);
        }
    }
}
