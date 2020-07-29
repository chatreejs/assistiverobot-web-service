using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssistiveRobot.Web.Service.Models.Request
{
    public class JobRequest
    {
        public long Start { get; set; }
        public long Destination { get; set; }
    }
}