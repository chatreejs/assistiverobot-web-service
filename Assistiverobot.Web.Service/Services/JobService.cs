using System;
using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Constants;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Params;
using AssistiveRobot.Web.Service.Models.Request;
using AssistiveRobot.Web.Service.Models.Response;
using AssistiveRobot.Web.Service.Repositories;

namespace AssistiveRobot.Web.Service.Services
{
    public class JobService
    {
        private readonly JobRepository _jobRepository;
        private readonly GoalRepository _goalRepository;

        public JobService(JobRepository jobRepository, GoalRepository goalRepository)
        {
            _jobRepository = jobRepository;
            _goalRepository = goalRepository;
        }

        public List<JobResponse> GetAllJob(JobFilter jobFilter)
        {
            try
            {
                var jobs = _jobRepository.GetAllByCondition(jobFilter);
                if (!jobs.Any())
                {
                    return null;
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

                return jobResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public JobResponse GetJobById(long id)
        {
            try
            {
                var job = _jobRepository.Get(id);
                if (job == null)
                {
                    return null;
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

                return jobResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public void CreateJob(JobLocationRequest jobLocationRequest)
        {
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
                    LocationId = jobLocationRequest.Start,
                    Status = "pending"
                };
                var destinationGoal = new Goal()
                {
                    JobId = job.JobId,
                    LocationId = jobLocationRequest.Destination,
                    Status = "pending"
                };
                _goalRepository.Add(startGoal);
                _goalRepository.Add(destinationGoal);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public void UpdateJob(long id, JobRequest jobRequest)
        {
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
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}