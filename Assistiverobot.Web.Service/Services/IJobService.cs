using System.Collections.Generic;
using AssistiveRobot.Web.Service.Models.Params;
using AssistiveRobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Models.Response;

namespace AssistiveRobot.Web.Service
{
    public interface IJobService
    {
        public List<JobResponse> GetAllJob(JobFilter jobFilter);
        public JobResponse GetJobById(long id);
        public void CreateJob(JobLocationRequest jobLocationRequest);
        public void UpdateJob(long id, JobRequest jobRequest);
    }
}