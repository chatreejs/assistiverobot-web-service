using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Params;
using Microsoft.EntityFrameworkCore;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AssistiveRobotContext _context;

        public JobRepository(AssistiveRobotContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Job> GetAll()
        {
            return _context.Job
                .Include(j => j.Goal)
                .ToList();
        }

        public IEnumerable<Job> GetAllByCondition(JobFilter jobFilter)
        {
            if (!string.IsNullOrEmpty(jobFilter.Status))
            {
                return _context.Job
                    .Include(j => j.Goal)
                    .Include("Goal.Location")
                    .Where(j => j.Status.Equals(jobFilter.Status))
                    .Take(jobFilter.Limit)
                    .ToList();
            }

            return _context.Job
                .Include(j => j.Goal)
                .Include("Goal.Location")
                .Take(jobFilter.Limit)
                .ToList();
        }

        public Job Get(long id)
        {
            var job = _context.Job
                .Include(j => j.Goal)
                .Include("Goal.Location")
                .SingleOrDefault(j => j.JobId == id);

            return job;
        }

        public void Add(Job entity)
        {
            _context.Job.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Job entityToUpdate, Job entity)
        {
            entityToUpdate = _context.Job
                .Single(j => j.JobId == entityToUpdate.JobId);

            entityToUpdate.Status = entity.Status;
            entityToUpdate.UpdatedDate = entity.UpdatedDate;

            _context.SaveChanges();
        }

        public void Delete(Job entity)
        {
            throw new System.NotImplementedException();
        }
    }
}