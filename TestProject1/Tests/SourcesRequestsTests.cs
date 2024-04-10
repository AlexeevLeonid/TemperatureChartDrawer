using TempAnAr.Persistence.Implementations;
using TempArAn.Application.Source.Handlers;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.Models.Source;
using TempArAn.Domain.Responses;
using TempArAn.Tests.Common;

namespace TempArAn.Tests.Tests
{
    [Collection("QueryCollection")]
    public class SourcesRequestsTests : BaseTest
    {
        public SourcesRequestsTests(QueryTestFixture fixture) : base(fixture) { }
        //[Fact]
        //public async Task CreateSourceCommandHandler_Success()
        //{
        //    var handler = new CreateHTMLSourceCommandHandler(new ApplicationUnitOfWork(context, mapper), mapper, new SourceValidator());

        //    var result = await handler.Handle(
        //        new CreateHTMLSourceCommand(
        //            new CreateHtmlSourceDetails("test", "test", "test", "test", 5),
        //        ApplicationContextFactory.UserA),
        //        CancellationToken.None);
        //    Assert.NotNull(context.Sources.FirstOrDefault(x => x.Name == "test"));
        //}

        [Fact]
        public async Task GetSourceQueryHandler_Success()
        {
            var handler = new GetSourceQueryHandler(new ApplicationUnitOfWork(context), mapper);
            var source = new HTMLSource("a", "a", "a", "a", ApplicationContextFactory.UserA.Id, 5);
            context.Sources.Add(source);
            context.SaveChanges();

            var result = await handler.Handle(
                new GetSourceQuery(source.Id,
                ApplicationContextFactory.UserA),
                CancellationToken.None);

            Assert.Equal(
                result.Id,
                mapper.Map<SourceResponse>(
                    context.Sources.FirstOrDefault(
                        x => x.Id == source.Id)).Id);
        }

        [Fact]
        public async Task DeleteSourceCommandHandler_Success()
        {
            var handler = new DeleteSourceCommandHandler(new ApplicationUnitOfWork(context));

            var result = await handler.Handle(
                new DeleteSourceCommand(
                    ApplicationContextFactory.Source.Id,
                    ApplicationContextFactory.UserA),
                CancellationToken.None);

            Assert.Null(context.Sources.FirstOrDefault(x => x.Id == ApplicationContextFactory.Source.Id));
        }

        [Fact]
        public async Task GetSourcesQueryHandler_Success()
        {
            var handler = new GetSourcesQueryHandler(new ApplicationUnitOfWork(context), mapper);

            var result = await handler.Handle(
                new GetSourcesQuery(
                    ApplicationContextFactory.UserA),
                CancellationToken.None);

            Assert.Equal(
                result.userList.Select(x => x.Id).ToList(),
                    context.Sources.Where(x => x.UserId == ApplicationContextFactory.UserA.Id).Select(x => x.Id).ToList());
        }
    }
}
