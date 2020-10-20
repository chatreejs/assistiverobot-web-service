using System.Collections.Generic;
using AssistiveRobot.Web.Service.Request;
using AssistiveRobot.Web.Service.Response;

namespace AssistiveRobot.Web.Service.Services
{
    public interface IUserService
    {
        public List<UserResponse> GetAllUser();
        public UserResponse GetUserById(long id);
        public void CreateUser(UserRequest model);
        public void UpdateUser(UserRequest model);
    }
}