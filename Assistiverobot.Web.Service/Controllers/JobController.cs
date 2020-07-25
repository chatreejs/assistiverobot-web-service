using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Assistiverobot.Web.Service.Constants;
using Assistiverobot.Web.Service.Domains;
using Assistiverobot.Web.Service.Models.Response;
using Assistiverobot.Web.Service.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assistiverobot.Web.Service.Controllers
{
    [ApiController]
    [Route("api/v1/jobs")]
    public class JobController : ControllerBase
    {
        private readonly JobRepository _jobRepository;

        public JobController(JobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            try
            {
                var jobs = _jobRepository.GetAll();
                var enumerable = jobs as Job[] ?? jobs.ToArray();
                if (!enumerable.Any())
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var jobResponse = enumerable
                    .Select(job => new JobResponse()
                    {
                        JobId = job.JobId,
                        Status = job.Status,
                        CreatedDate = job.CreatedDate,
                        UpdatedDate = job.UpdatedDate
                    })
                    .ToList();
                
                return StatusCode(StatusCodes.Status200OK, ResultResponse.GetResultSuccess(jobResponse));
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
                    return StatusCode(StatusCodes.Status404NotFound, null);
                }

                var jobResponse = new JobResponse()
                {
                    JobId = job.JobId,
                    Status = job.Status,
                    CreatedDate = job.CreatedDate,
                    UpdatedDate = job.UpdatedDate
                };
                return StatusCode(StatusCodes.Status200OK, jobResponse);
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
            return StatusCode(StatusCodes.Status200OK, job);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateJob(long id, [FromBody] object job)
        {
            return StatusCode(StatusCodes.Status200OK, job);
        }
    }
}