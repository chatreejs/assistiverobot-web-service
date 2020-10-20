namespace AssistiveRobot.Web.Service.Models.Response
{
    public class ErrorResponse
    {
        public string Error { get; set; }
        public string Message { get; set; }

        public ErrorResponse(string error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}