using System;
using System.Collections.Generic;

namespace AssistiveRobot.Web.Service.Domains
{
    public partial class Location
    {
        public Location()
        {
            Goal = new HashSet<Goal>();
        }

        public long LocationId { get; set; }
        public decimal? PositionX { get; set; }
        public decimal? PositionY { get; set; }
        public decimal? PositionZ { get; set; }
        public decimal? OrientationX { get; set; }
        public decimal? OrientationY { get; set; }
        public decimal? OrientationZ { get; set; }
        public decimal? OrientationW { get; set; }

        public virtual ICollection<Goal> Goal { get; set; }
    }
}
