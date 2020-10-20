using System;
using System.Collections.Generic;
using System.Linq;
using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly AssistiveRobotContext _context;

        public UserTokenRepository(AssistiveRobotContext context)
        {
            _context = context;
        }

        public UserToken Add(string refreshToken, long userId, string nonce, int refreshTokenExpires, string checksum)
        {
            throw new System.NotImplementedException();
        }

        public UserToken Get(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public UserToken Get(long userId, string nonce)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserToken> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetRefreshTokens(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsAlive(long userId)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(long userId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveExpired()
        {
            // var tokenExpired = _context.UserToken
            //     .Where(r => r.Expires.CompareTo(DateTime.Now) < 0)
            //     .ToList();
            // _context.RemoveRange(tokenExpired);
            throw new System.NotImplementedException();
        }

        public void UpdateFcmToken(int userId, string nonce, string fcmToken)
        {
            throw new System.NotImplementedException();
        }
    }
}