using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class LocationRepository : IRepository<Location>
    {
        private readonly AssistiveRobotContext _context;

        public LocationRepository(AssistiveRobotContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Location.ToList();
        }

        public Location Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Location entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Location entityToUpdate, Location entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Location entity)
        {
            throw new System.NotImplementedException();
        }
    }
}