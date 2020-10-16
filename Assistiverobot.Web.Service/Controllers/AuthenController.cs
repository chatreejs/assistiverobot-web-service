using System.Threading.Tasks;
using AssistiveRobot.Web.Service.Models.OAuth;
using AssistiveRobot.Web.Service.Request;
using AssistiveRobot.Web.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenController : BaseController
    {
        private readonly IAuthenService _authenService;
        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpGet(".well-known/openid-configuration")]
        [ProducesResponseType(typeof(OpenidConfiguration), StatusCodes.Status200OK)]
        public async Task<OpenidConfiguration> WellKnown()
        {
            return await _authenService.WellKnow();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            return _authenService.Login(this, model);
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest model)
        {
            return _authenService.RefreshToken(this, model);
        }
    }
}