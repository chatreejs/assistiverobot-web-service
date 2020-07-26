using System;
using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Models.Params;
using Microsoft.EntityFrameworkCore;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class JobRepository : IDataRepository<Job>
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

        // public IEnumerable<Job> GetAllByFilter(JobFilter filter)
        // {
        //     return _context.Job
        //         .Include(j => j.Goal)
        //         .Where(j => j.Status.Equals(filter.Status ?? String.Empty))
        //         .Take(filter.Limit)
        //         .ToList();
        // }

        public Job Get(long id)
        {
            var job = _context.Job
                .Include(j => j.Goal)
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
            throw new System.NotImplementedException();
        }

        public void Delete(Job entity)
        {
            throw new System.NotImplementedException();
        }
    }
}