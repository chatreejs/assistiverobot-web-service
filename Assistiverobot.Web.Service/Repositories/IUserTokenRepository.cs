using System.Collections.Generic;
using AssistiveRobot.Web.Service.Domains;

namespace AssistiveRobot.Web.Service.Repositories
{
    public interface IUserTokenRepository
    {
        UserToken Add(string refreshToken, long userId, string nonce, int refreshTokenExpires, string checksum);
        UserToken Get(string refreshToken);
        UserToken Get(long userId, string nonce);
        IEnumerable<UserToken> GetAll();
        bool IsAlive(long userId);
        void Remove(string refreshToken);
        void Remove(long userId);
        void RemoveExpired();
        void UpdateFcmToken(int userId, string nonce, string fcmToken);
        IEnumerable<string> GetRefreshTokens(int userId);
    }
}