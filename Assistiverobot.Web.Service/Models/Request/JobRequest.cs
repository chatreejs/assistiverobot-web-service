using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssistiveRobot.Web.Service.Models.Request
{
    public class JobRequest
    {
        public ICollection<GoalRequest> Goal = new List<GoalRequest>();
    }
}