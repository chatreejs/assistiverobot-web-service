using System.Threading.Tasks;
using AssistiveRobot.Web.Service.Controllers;
using AssistiveRobot.Web.Service.Models.OAuth;
using AssistiveRobot.Web.Service.Request;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Services
{
    public interface IAuthenService
    {
        Task<OpenidConfiguration> WellKnow();
        IActionResult Login(BaseController baseController, LoginRequest model);
        IActionResult RefreshToken(BaseController baseController, RefreshTokenRequest model);
    }
}