using AssistiveRobot.Web.Service.Constants;

namespace AssistiveRobot.Web.Service.Models.Response
{
    public class ResultResponse
    {
        public string Message { get; set; }
        public object Result { get; set; }
        public static object GetResultSuccess(object result = null)
        {
            return new ResultResponse()
            {
                Message = StatusMessage.MESSAGE_SUCCESS,
                Result = result,
            };
        }

        public static object GetResultInternalError()
        {
            return new ResultResponse()
            {
                Message = StatusMessage.MESSAGE_INTERNAL_ERROR,
                Result = null
            };
        }
    }
}
