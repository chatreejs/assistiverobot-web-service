using AssistiveRobot.Web.Service.Request;
using AssistiveRobot.Web.Service.Services;
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