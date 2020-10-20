using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class GoalRepository : IGoalRepository
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
            var goal = _context.Goal
                .SingleOrDefault(g => g.GoalId == id);

            return goal;
        }

        public void Add(Goal entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Goal entityToUpdate, Goal entity)
        {
            entityToUpdate = _context.Goal
                .Single(g => g.GoalId == entityToUpdate.GoalId);

            entityToUpdate.Status = entity.Status;

            _context.SaveChanges();
        }

        public void Delete(Goal entity)
        {
            throw new System.NotImplementedException();
        }
    }
}