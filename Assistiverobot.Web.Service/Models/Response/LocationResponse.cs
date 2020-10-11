namespace AssistiveRobot.Web.Service.Models.Response
{
    public class LocationResponse
    {
        public long LocationId { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public Orientation Orientation { get; set; }
    }
}