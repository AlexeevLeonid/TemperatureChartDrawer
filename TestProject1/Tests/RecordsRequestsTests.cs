using TempAnAr.Persistence.Implementations;
using TempArAn.Application.Records.Handlers;
using TempArAn.Application.Records.Requests;
using TempArAn.Domain.Models.Record;
using TempArAn.Domain.Responses;
using TempArAn.Services.Services.ConvertService;
using TempArAn.Tests.Common;

namespace TempArAn.Tests.Tests
{
    [Collection("QueryCollection")]
    public class RecordsRequestsTests : BaseTest
    {

        public RecordsRequestsTests(QueryTestFixture fixture) : base(fixture) { }

        [Fact]
        public async Task GetRecordsQueryHandler_Success()
        {
            var handler = new GetRecordsFromSourceQueryHandler(new ApplicationUnitOfWork(context), mapper);

            var result = await handler.Handle(
                new GetRecordsFromSourceQuery(ApplicationContextFactory.Source.Id,
                ApplicationContextFactory.UserA)
                , CancellationToken.None);

            Assert.Equal(
                result.Select(x => x.Value),
                mapper.Map<List<SimpleRecordResponse>>(
                    context.DoubleRecords.Where(x => x.SourceId == ApplicationContextFactory.Source.Id).ToList()).Select(x => x.Value));
        }

        [Fact]
        public async Task GetErrorRecordsQueryHandler_Success()
        {
            var handler = new GetErrorsFromSourceQueryHandler(new ApplicationUnitOfWork(context), mapper);

            var result = await handler.Handle(
                new GetErrorsFromSourceQuery(ApplicationContextFactory.Source.Id,
                ApplicationContextFactory.UserA)
                , CancellationToken.None);

            Assert.Equal(
                result.Select(x => x.Value),
                mapper.Map<List<SimpleRecordResponse>>(
                    context.ErrorRecords.Where(x => x.SourceId == ApplicationContextFactory.Source.Id).ToList()).Select(x => x.Value));
        }

        [Fact]
        public async Task GetDataRecordsQueryHandler_Success()
        {
            var handler = new GetDataForLastMonthQueryHandler(new ApplicationUnitOfWork(context), mapper);
            var dataRecords = new Stack<TemperatureDataSetRecords>();
            var records = (await unitOfWork.DoubleRecords.GetRecordsForTimeAsync(
                DateTime.Now.Subtract(TimeSpan.FromDays(7)), DateTime.Now)).ToList();
            RecordConverterHostedService.ConvertingAsync(records, dataRecords);
            context.DataRecords.AddRange(dataRecords);
            context.SaveChanges();
            var result = await handler.Handle(
                new GetDataForLastMonthQuery(ApplicationContextFactory.Source.Id,
                ApplicationContextFactory.UserA)
                , CancellationToken.None);

            Assert.Equal(
                result.Select(x => x.Value),
                mapper.Map<List<ComplexRecordResponse>>(
                    context.DataRecords.Where(
                        x => x.SourceId == ApplicationContextFactory.Source.Id).
                        ToList()).
                        Select(x => x.Value));
        }
    }
}
