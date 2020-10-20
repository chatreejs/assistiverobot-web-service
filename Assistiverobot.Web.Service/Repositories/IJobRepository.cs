using System.Collections.Generic;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Params;

namespace AssistiveRobot.Web.Service.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
        public IEnumerable<Job> GetAllByCondition(JobFilter model);
    }
}