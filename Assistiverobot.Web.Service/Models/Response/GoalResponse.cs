using AssistiveRobot.Web.Service.Models.Request;

namespace AssistiveRobot.Web.Service.Models.Response
{
    public class GoalResponse
    {
        public long GoalId { get; set; }
        public Position Position { get; set; }
        public Orientation Orientation { get; set; }
        public string Status { get; set; }
    }
}