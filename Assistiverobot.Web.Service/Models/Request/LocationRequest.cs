using AssistiveRobot.Web.Service.Models.Response;

namespace Assistiverobot.Web.Service.Models.Request
{
    public class LocationRequest
    {
        public string Name { get; set; } = null;
        public Position Position { get; set; } = null;
        public Orientation Orientation { get; set; } = null;
    }
}
