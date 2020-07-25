using System;
using System.Collections.Generic;

namespace Assistiverobot.Web.Service.Domains
{
    public partial class Job
    {
        public Job()
        {
            Goal = new HashSet<Goal>();
        }

        public long JobId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Goal> Goal { get; set; }
    }
}
