using AssistiveRobot.Web.Service.Constants;
using AssistiveRobot.Web.Service.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult GetResultSuccess()
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageSuccess,
                Result = null,
            };
            return StatusCode(StatusCodes.Status200OK, resultResponse);
        }
        
        public IActionResult GetResultSuccess(object result)
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageSuccess,
                Result = result,
            };
            return StatusCode(StatusCodes.Status200OK, resultResponse);
        }
        
        public IActionResult GetResultSuccess(object result, int statusCode)
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageSuccess,
                Result = result,
            };
            return StatusCode(statusCode, resultResponse);
        }

        public IActionResult GetResultNotFound()
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageNotFound,
                Result = null
            };
            return StatusCode(StatusCodes.Status404NotFound, resultResponse);
        }

        public IActionResult GetResultInternalError()
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageInternalError,
                Result = null
            };
            return StatusCode(StatusCodes.Status500InternalServerError, resultResponse);
        }
    }
}