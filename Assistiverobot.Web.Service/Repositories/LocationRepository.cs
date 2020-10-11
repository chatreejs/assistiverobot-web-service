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
            var location = _context.Location
                .SingleOrDefault(l => l.LocationId == id);

            return location;
        }

        public void Add(Location entity)
        {
            _context.Location.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Location entityToUpdate, Location entity)
        {
            entityToUpdate = _context.Location
            .Single(l => l.LocationId == entityToUpdate.LocationId);

            entityToUpdate.Name = entity.Name;
            entityToUpdate.PositionX = entity.PositionX;
            entityToUpdate.PositionY = entity.PositionY;
            entityToUpdate.PositionZ = entity.PositionZ;
            entityToUpdate.OrientationX = entity.OrientationX;
            entityToUpdate.OrientationY = entity.OrientationY;
            entityToUpdate.OrientationZ = entity.OrientationZ;
            entityToUpdate.OrientationW = entity.OrientationW;

            _context.SaveChanges();
        }

        public void Delete(Location entity)
        {
            throw new System.NotImplementedException();
        }
    }
}