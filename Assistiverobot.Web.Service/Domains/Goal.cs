using System;
using System.Collections.Generic;

namespace AssistiveRobot.Web.Service.Domains
{
    public partial class Goal
    {
        public long GoalId { get; set; }
        public long? JobId { get; set; }
        public decimal? PositionX { get; set; }
        public decimal? PositionY { get; set; }
        public decimal? PositionZ { get; set; }
        public decimal? OrientationX { get; set; }
        public decimal? OrientationY { get; set; }
        public decimal? OrientationZ { get; set; }
        public decimal? OrientationW { get; set; }
        public string Status { get; set; }

        public virtual Job Job { get; set; }
    }
}
