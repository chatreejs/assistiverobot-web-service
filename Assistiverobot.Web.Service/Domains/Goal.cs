namespace AssistiveRobot.Web.Service.Domains
{
    public partial class Goal
    {
        public long GoalId { get; set; }
        public long JobId { get; set; }
        public long LocationId { get; set; }
        public string Status { get; set; }

        public virtual Job Job { get; set; }
        public virtual Location Location { get; set; }
    }
}
