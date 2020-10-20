using System;
using AssistiveRobot.Web.Service.Constants;
using AssistiveRobot.Web.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var userResponse = _userService.GetAllUser();
                if (userResponse == null)
                {
                    return GetResultSuccess(null, StatusCodes.Status204NoContent);
                }
                return GetResultSuccess(userResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }
    }
}