namespace AssistiveRobot.Web.Service.Models.Params
{
    public class JobFilter
    {
        public string Status { get; set; } = null;
        public int Limit { get; set; } = 100;
    }
}