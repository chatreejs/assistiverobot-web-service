using System;
namespace Assistiverobot.Web.Service.Models.Request
{
    public class LocationRequest
    {
        public string Name { get; set; } = null;
        public Position Position { get; set; } = null;
        public Orientation Orientation { get; set; } = null;
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
