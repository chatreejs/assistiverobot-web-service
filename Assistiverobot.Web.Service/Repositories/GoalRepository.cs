using System.Collections.Generic;
using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class GoalRepository : IRepository<Goal>
    {
        private readonly AssistiveRobotContext _context;

        public GoalRepository(AssistiveRobotContext context)
        {
            _context = context;
        }

        public IEnumerable<Goal> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Goal Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Goal entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Goal entityToUpdate, Goal entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Goal entity)
        {
            throw new System.NotImplementedException();
        }
    }
}