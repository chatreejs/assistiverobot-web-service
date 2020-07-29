namespace AssistiveRobot.Web.Service.Models.Response
{
    public class GoalResponse
    {
        public long GoalId { get; set; }
        public decimal? PositionX { get; set; }
        public decimal? PositionY { get; set; }
        public decimal? PositionZ { get; set; }
        public decimal? OrientationX { get; set; }
        public decimal? OrientationY { get; set; }
        public decimal? OrientationZ { get; set; }
        public decimal? OrientationW { get; set; }
        public string Status { get; set; }
    }
}