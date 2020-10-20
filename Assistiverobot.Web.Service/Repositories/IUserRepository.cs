using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public User Get(string username, string password);
    }
}