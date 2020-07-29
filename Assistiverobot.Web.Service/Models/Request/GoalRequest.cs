using AssistiveRobot.Web.Service.Models.Response;

namespace AssistiveRobot.Web.Service.Models.Request
{
    public class GoalRequest
    {
        public Position Position = new Position();
        public Orientation Orientation = new Orientation();
    }
}