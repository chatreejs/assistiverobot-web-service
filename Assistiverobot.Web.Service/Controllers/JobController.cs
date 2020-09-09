using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssistiveRobot.Web.Service.Constants;
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
        private readonly GoalRepository _goalRepository;

        public JobController(JobRepository jobRepository, GoalRepository goalRepository)
        {
            _jobRepository = jobRepository;
            _goalRepository = goalRepository;
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

        [HttpPost]
        public IActionResult CreateJob([FromBody] JobLocationRequest jobRequest)
        {
            if (jobRequest.Start == 0 || jobRequest.Destination == 0)
            {
                return GetResultBadRequest();
            }

            try
            {
                var job = new Job()
                {
                    Status = JobStatus.StatusPending,
                    CreatedDate = DateTime.Now
                };
                _jobRepository.Add(job);

                var startGoal = new Goal()
                {
                    JobId = job.JobId,
                    LocationId = jobRequest.Start,
                    Status = "pending"
                };
                var destinationGoal = new Goal()
                {
                    JobId = job.JobId,
                    LocationId = jobRequest.Destination,
                    Status = "pending"
                };
                _goalRepository.Add(startGoal);
                _goalRepository.Add(destinationGoal);
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
                var job = _jobRepository.Get(id);
                var jobUpdated = new Job()
                {
                    JobId = job.JobId,
                    Status = jobRequest.Status,
                    CreatedDate = job.CreatedDate,
                    UpdatedDate = DateTime.Now
                };
                _jobRepository.Update(job, jobUpdated);
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