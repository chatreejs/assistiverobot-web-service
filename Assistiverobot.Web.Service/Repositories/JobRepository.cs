using System.Collections.Generic;
using System.Linq;
using Assistiverobot.Web.Service.Domains;
using Microsoft.EntityFrameworkCore;

namespace Assistiverobot.Web.Service.Repositories
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