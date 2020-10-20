using System;
using System.Linq;
using System.Reflection;
using AssistiveRobot.Web.Service.Constants;
using AssistiveRobot.Web.Service.Models.Params;
using AssistiveRobot.Web.Service.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/jobs")]
    public class JobController : BaseController
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public IActionResult GetAllJobs([FromQuery] JobFilter jobFilter)
        {
            try
            {
                var jobResponse = _jobService.GetAllJob(jobFilter);
                if (jobResponse == null)
                {
                    return GetResultSuccess(null, StatusCodes.Status204NoContent);
                }
                return GetResultSuccess(jobResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetJobById(long id)
        {
            try
            {
                var jobResponse = _jobService.GetJobById(id);
                if (jobResponse == null)
                {
                    return GetResultNotFound();
                }
                return GetResultSuccess(jobResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] JobLocationRequest jobLocationRequest)
        {
            if (jobLocationRequest.Start == 0 || jobLocationRequest.Destination == 0)
            {
                return GetResultBadRequest();
            }

            try
            {
                _jobService.CreateJob(jobLocationRequest);
                return GetResultCreated();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateJob(long id, [FromBody] JobRequest jobRequest)
        {
            if (jobRequest.Status == null)
            {
                return GetResultBadRequest();
            }

            var hasField = typeof(JobStatus).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => x.GetRawConstantValue().ToString()).Contains(jobRequest.Status);

            if (!hasField)
            {
                return GetResultBadRequest();
            }

            try
            {
                var job = _jobService.GetJobById(id);
                if (job == null)
                {
                    return GetResultNotFound();
                }
                _jobService.UpdateJob(id, jobRequest);
                return GetResultSuccess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }
    }
}