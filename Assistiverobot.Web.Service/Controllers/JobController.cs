using System;
using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Params;
using AssistiveRobot.Web.Service.Models.Request;
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
        public IActionResult GetAllJobs([FromQuery] JobFilter jobFilter)
        {
            try
            {
                var jobs = _jobRepository.GetAllByCondition(jobFilter);
                if (!jobs.Any())
                {
                    return GetResultSuccess(null, StatusCodes.Status204NoContent);
                }

                var jobResponse = new List<JobResponse>();
                foreach (var job in jobs)
                {
                    var goalResponse = new List<GoalResponse>();
                    foreach (var goal in job.Goal)
                    {
                        var position = new Position()
                        {
                            X = goal.Location.PositionX,
                            Y = goal.Location.PositionY,
                            Z = goal.Location.PositionZ,
                        };
                        var orientation = new Orientation()
                        {
                            X = goal.Location.OrientationX,
                            Y = goal.Location.OrientationY,
                            Z = goal.Location.OrientationZ,
                            W = goal.Location.OrientationW,
                        };
                        goalResponse.Add(new GoalResponse()
                        {
                            GoalId = goal.GoalId,
                            Position = position,
                            Orientation = orientation,
                            Status = goal.Status
                        });
                    }

                    jobResponse.Add(new JobResponse()
                    {
                        JobId = job.JobId,
                        Goal = goalResponse,
                        Status = job.Status,
                        CreatedDate = job.CreatedDate,
                        UpdatedDate = job.UpdatedDate
                    });
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
                var job = _jobRepository.Get(id);
                if (job == null)
                {
                    return GetResultNotFound();
                }

                var goalResponse = new List<GoalResponse>();
                foreach (var goal in job.Goal)
                {
                    var position = new Position()
                    {
                        X = goal.Location.PositionX,
                        Y = goal.Location.PositionY,
                        Z = goal.Location.PositionZ,
                    };
                    var orientation = new Orientation()
                    {
                        X = goal.Location.OrientationX,
                        Y = goal.Location.OrientationY,
                        Z = goal.Location.OrientationZ,
                        W = goal.Location.OrientationW,
                    };
                    goalResponse.Add(new GoalResponse()
                    {
                        GoalId = goal.GoalId,
                        Position = position,
                        Orientation = orientation,
                        Status = goal.Status
                    });
                }

                var jobResponse = new JobResponse()
                {
                    JobId = job.JobId,
                    Goal = goalResponse,
                    Status = job.Status,
                    CreatedDate = job.CreatedDate,
                    UpdatedDate = job.UpdatedDate
                };
                return GetResultSuccess(jobResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetResultInternalError();
            }
        }

        // TODO: แก้เป็นรับ startLocationId, destinationLocationId
        [HttpPost]
        public IActionResult CreateJob([FromBody] JobRequest jobRequest)
        {
            if (!ModelState.IsValid)
            {
                return GetResultBadRequest();
            }

            var job = new Job()
            {
                Status = "pending",
                CreatedDate = DateTime.Now
            };
            _jobRepository.Add(job);
            Console.WriteLine(job.JobId);
            return GetResultCreated();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateJob(long id, [FromBody] object job)
        {
            return GetResultSuccess();
        }
    }
}