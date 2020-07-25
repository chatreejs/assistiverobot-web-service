using System;
using Newtonsoft.Json;

namespace Assistiverobot.Web.Service.Models.Response
{
    public class JobResponse
    {
        public long JobId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}