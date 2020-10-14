using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AssistiveRobotContext _context;

        public UserRepository(AssistiveRobotContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User Get(string username, string password)
        {
            return _context.User
                .FirstOrDefault(u =>
                    (u.Username == username) &&
                    (u.Password == password));
        }

        public User Get(long id)
        {
            var user = _context.User
                .SingleOrDefault(u => u.UserId == id);

            return user;
        }

        public void Add(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User entityToUpdate, User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}