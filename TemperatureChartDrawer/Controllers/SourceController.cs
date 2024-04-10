using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempArAn.Application.Records.Requests;
using TempArAn.Application.Source.Requests;
using TempArAn.Domain.Requests;

namespace TempAnAr.Controllers
{
    [ApiController]
    [Route("api/source")]
    [Authorize]
    public class SourceController : BaseController
    {

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSource(Guid id)
        {
            var query = new GetSourceQuery(id, AuthUser);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("records/{id}")]
        public async Task<IActionResult> GetRecordsFromSource(Guid id)
        {
            var query = new GetRecordsFromSourceQuery(id, AuthUser);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("data/{id}")]
        public async Task<IActionResult> GetDataFromSource(Guid id)
        {
            var query = new GetDataForLastMonthQuery(id, AuthUser);
            return Ok(await Mediator.Send(query));
        }


        [HttpGet("data/{id}/months")]
        public async Task<IActionResult> GetDataForMonthFromSource(Guid id)
        {
            var query = new GetAllDataQuery(id, AuthUser);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("errors/{id}")]
        public async Task<IActionResult> GetErrorFromSource(Guid id)
        {
            var query = new GetErrorsFromSourceQuery(id, AuthUser);
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSources()
        {
            GetSourcesQuery query;
            if (User.Identity != null && !User.Identity.IsAuthenticated) query = new GetSourcesQuery(null);
            else query = new GetSourcesQuery(AuthUser);
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("")]
        public async Task<IActionResult> PostSource([FromBody] CreateHtmlSourceDetails details)
        {
            var command = new CreateHTMLSourceCommand(details, AuthUser);
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSource(Guid id)
        {
            var request = new DeleteSourceCommand(id, AuthUser);
            return Ok(await Mediator.Send(request));
        }
    }
}
