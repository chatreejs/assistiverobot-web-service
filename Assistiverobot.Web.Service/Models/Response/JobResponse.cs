using System;
using Newtonsoft.Json;

namespace AssistiveRobot.Web.Service.Models.Response
{
    public class JobResponse
    {
        public long JobId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}