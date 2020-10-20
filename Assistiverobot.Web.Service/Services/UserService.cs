using System;
using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Repositories;
using AssistiveRobot.Web.Service.Request;
using AssistiveRobot.Web.Service.Response;

namespace AssistiveRobot.Web.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserResponse> GetAllUser()
        {
            try
            {
                var users = _userRepository.GetAll();
                if (!users.Any())
                {
                    return null;
                }
                var userResponse = new List<UserResponse>();
                foreach (var user in users)
                {
                    userResponse.Add(new UserResponse()
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role,
                        Username = user.Username
                    });
                }

                return userResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public UserResponse GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(UserRequest model)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserRequest model)
        {
            throw new NotImplementedException();
        }
    }
}