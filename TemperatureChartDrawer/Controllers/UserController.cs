using Microsoft.AspNetCore.Mvc;
using TempArAn.Application.User.Requests;
using TempArAn.Domain.Requests;

namespace TempAnAr.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDetails details)
        {
            var request = new LoginCommand(details);
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDetails details)
        {
            var request = new RegisterCommand(details);
            return Ok(await Mediator.Send(request));

        }

        [HttpGet("taken")]
        public async Task<IActionResult> IsUsernameNotTaken(string username)
        {
            var request = new IsNotTakenQuery(username);
            return Ok(await Mediator.Send(request));
        }


    }
}
