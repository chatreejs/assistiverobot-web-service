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
                Success = true
            };
            return StatusCode(StatusCodes.Status200OK, resultResponse);
        }

        public IActionResult GetResultSuccess(object result)
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageSuccess,
                Result = result,
                Success = true
            };
            return StatusCode(StatusCodes.Status200OK, resultResponse);
        }

        public IActionResult GetResultSuccess(object result, int statusCode)
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageSuccess,
                Result = result,
                Success = true
            };
            return StatusCode(statusCode, resultResponse);
        }

        public IActionResult GetResultCreated()
        {
            return GetResultSuccess(null, StatusCodes.Status201Created);
        }

        public IActionResult GetResultBadRequest()
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageInvalidRequestParams,
                Result = null,
                Success = false
            };
            return StatusCode(StatusCodes.Status400BadRequest, resultResponse);
        }

        public IActionResult GetResultBadRequest(ErrorResponse errorResponse)
        {
            return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
        }

        public IActionResult GetResultNotFound()
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageNotFound,
                Result = null,
                Success = false
            };
            return StatusCode(StatusCodes.Status404NotFound, resultResponse);
        }

        public IActionResult GetResultInternalError()
        {
            var resultResponse = new ResultResponse()
            {
                Message = StatusMessages.MessageInternalError,
                Result = null,
                Success = false
            };
            return StatusCode(StatusCodes.Status500InternalServerError, resultResponse);
        }
    }
}