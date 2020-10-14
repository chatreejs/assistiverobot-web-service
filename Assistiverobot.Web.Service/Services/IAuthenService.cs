using AssistiveRobot.Web.Service.Controllers;
using AssistiveRobot.Web.Service.Core;
using AssistiveRobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Repositories;
using AssistiveRobot.Web.Service.Request;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Services
{
    public interface IAuthenService
    {    
        IActionResult Login(BaseController baseController, LoginRequest model);
        IActionResult RefreshToken(BaseController baseController, RefreshTokenRequest model);
    }
}