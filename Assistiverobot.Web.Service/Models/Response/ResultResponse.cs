using Assistiverobot.Web.Service.Constants;

namespace Assistiverobot.Web.Service.Models.Response
{
    public class ResultResponse
    {
        public string Message { get; set; }
        public object Result { get; set; }
        public static object GetResultSuccess(object result)
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