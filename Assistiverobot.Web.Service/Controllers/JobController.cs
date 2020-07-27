using System;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Params;
using AssistiveRobot.Web.Service.Models.Response;
using AssistiveRobot.Web.Service.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistiveRobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/jobs")]
    public class JobController : BaseController
    {
        private readonly JobRepository _jobRepository;

        public JobController(JobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public IActionResult GetAllJobs([FromQuery] JobFilter filter)
        {
            try
            {
                var jobs = _jobRepository.GetAll();
                var enumerable = jobs as Job[] ?? jobs.ToArray();
                if (!enumerable.Any())
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var jobResponse = (from job in jobs
                    let goalResponse = job.Goal.Select(goal => new GoalResponse()
                        {
                            GoalId = goal.GoalId,
                            JobId = goal.JobId,
                            PositionX = goal.PositionX,
                            PositionY = goal.PositionY,
                            PositionZ = goal.PositionZ,
                            OrientationX = goal.OrientationX,
                            OrientationY = goal.OrientationY,
                            OrientationZ = goal.OrientationZ,
                            OrientationW = goal.OrientationW,
                            Status = goal.Status
                        })
                        .ToList()
                    select new JobResponse()
                    {
                        JobId = job.JobId,
                        Goal = goalResponse,
                        Status = job.Status,
                        CreatedDate = job.CreatedDate,
                        UpdatedDate = job.UpdatedDate
                    }).ToList();
                return GetResultSuccess(jobResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetJobById(long id)
        {
            try
            {
                var job = _jobRepository.Get(id);
                if (job == null)
                {
                    // return StatusCode(StatusCodes.Status404NotFound, null);
                    return GetResultSuccess(null, StatusCodes.Status404NotFound);
                }

                var goalResponse = job.Goal.Select(goal => new GoalResponse()
                    {
                        GoalId = goal.GoalId,
                        JobId = goal.JobId,
                        PositionX = goal.PositionX,
                        PositionY = goal.PositionY,
                        PositionZ = goal.PositionZ,
                        OrientationX = goal.OrientationX,
                        OrientationY = goal.OrientationY,
                        OrientationZ = goal.OrientationZ,
                        OrientationW = goal.OrientationW,
                        Status = goal.Status
                    })
                    .ToList();
                var jobResponse = new JobResponse()
                {
                    JobId = job.JobId,
                    Goal = goalResponse,
                    Status = job.Status,
                    CreatedDate = job.CreatedDate,
                    UpdatedDate = job.UpdatedDate
                };
                // return StatusCode(StatusCodes.Status200OK, jobResponse);
                return GetResultSuccess(jobResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] object job)
        {
            return GetResultSuccess();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateJob(long id, [FromBody] object job)
        {
            return GetResultSuccess();
        }
    }
}