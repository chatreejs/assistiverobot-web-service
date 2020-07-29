namespace AssistiveRobot.Web.Service.Models.Request
{
    public class GoalRequest
    {
        public Position Position = new Position();
        public Orientation Orientation = new Orientation();
    }

    public class Position
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
    }

    public class Orientation
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
        public decimal W { get; set; }
    }
}