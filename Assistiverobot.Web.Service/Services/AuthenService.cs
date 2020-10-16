using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AssistiveRobot.Web.Service.Controllers;
using AssistiveRobot.Web.Service.Core;
using AssistiveRobot.Web.Service.Domains;
using AssistiveRobot.Web.Service.Extensions;
using AssistiveRobot.Web.Service.Models.OAuth;
using AssistiveRobot.Web.Service.Models.Response;
using AssistiveRobot.Web.Service.Repositories;
using AssistiveRobot.Web.Service.Request;
using AssistiveRobot.Web.Service.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AssistiveRobot.Web.Service.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly UserRepository _userRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppSettings _appSettings;
        public AuthenService(IUserTokenRepository userTokenRepository,
                            UserRepository userRepository,
                            IWebHostEnvironment hostingEnvironment,
                            IOptions<AppSettings> appSettings)
        {
            _userTokenRepository = userTokenRepository;
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
            _appSettings = appSettings.Value;
        }

        public async Task<OpenidConfiguration> WellKnow()
        {
            string filePath = _hostingEnvironment.ContentRootPath;
            filePath = Path.Join(filePath, "Config", "openid-configuration.json");

            string json = await System.IO.File.ReadAllTextAsync(filePath);
            json = json.Replace("{issuer_endpoint}", $"{_appSettings.OAuth.Issuer}/api/v1/authen");

            return JsonSerializer.Deserialize<OpenidConfiguration>(json);
        }

        public IActionResult Login(BaseController baseController, LoginRequest model)
        {
            User user;

            if (model.Username == null || model.Password == null)
            {
                return baseController.GetResultBadRequest(new ErrorResponse("invalid_request", "The request is missing a required parameter."));
            }

            user = _userRepository.Get(model.Username, model.Password);

            if (user == null)
            {
                return baseController.GetResultBadRequest(new ErrorResponse("invalid_grant", "The username or password is incorrect."));
            }

            long userId = user.UserId;
            string userRole = user.Role;

            return baseController.Ok(GenerateAccessTokenResponse(_userTokenRepository, _appSettings.OAuth, userId, userRole, user.Username));
        }

        public IActionResult RefreshToken(BaseController baseController, RefreshTokenRequest model)
        {
            if (model.RefreshToken == null)
            {
                return baseController.GetResultBadRequest(new ErrorResponse("invalid_request", "The request is missing a required parameter, includes an unsupported parameter value (other than grant type)."));
            }

            UserToken existsRefreshToken = _userTokenRepository.Get(refreshToken: model.RefreshToken);

            if (existsRefreshToken == null)
            {
                return baseController.GetResultBadRequest(new ErrorResponse("invalid_grant", "Invalid refresh_token or expired."));
            }

            User user = _userRepository.Get(existsRefreshToken.UserId);
            string username = user.Username;
            string userRole = user.Role;

            if (existsRefreshToken.CheckSum != (existsRefreshToken.RefreshToken + username).GetSHA256HashString())
            {
                return baseController.GetResultBadRequest(new ErrorResponse("invalid_grant", "Invalid refresh_token."));
            }

            if (existsRefreshToken.Expires.CompareTo(DateTime.Now) < 0)
            {
                return baseController.GetResultBadRequest(new ErrorResponse("invalid_grant", "The refresh_token has expired."));
            }

            // Remove old refresh token
            _userTokenRepository.Remove(model.RefreshToken);

            return baseController.Ok(GenerateAccessTokenResponse(_userTokenRepository, _appSettings.OAuth, existsRefreshToken.UserId, userRole, username));
        }

        private static string GenerateAccessToken(long userId, string userRole, string nonce, string secretKey, string issuer, int expiresAfter)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddSeconds(expiresAfter),
                signingCredentials: signingCredentials)
            {
                Payload =
                {
                    ["sub"] = userId,
                    ["iat"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    ["nonce"] = nonce
                }
            };

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            string token = Guid.NewGuid().ToString("n");
            return token;
        }

        private static string GenerateNonce()
        {
            string nonce = Guid.NewGuid().ToString("n");
            return nonce;
        }

        private static TokenResponse GenerateAccessTokenResponse(IUserTokenRepository userTokenRepository, IOAuth appSettingsOAuth, long userId, string userRole, string username)
        {
            string nonce = GenerateNonce();
            string accessToken = GenerateAccessToken(
                userId: userId,
                userRole: userRole,
                nonce: nonce,
                secretKey: appSettingsOAuth.SecretKey,
                issuer: appSettingsOAuth.Issuer,
                expiresAfter: appSettingsOAuth.AccessTokenExpires);
            string refreshToken = GenerateRefreshToken();
            string checksum = (refreshToken + username).GetSHA256HashString();
            userTokenRepository.Add(refreshToken, userId, nonce, appSettingsOAuth.RefreshTokenExpires, checksum);
            userTokenRepository.RemoveExpired();

            return new TokenResponse
            {
                AccessToken = accessToken,
                TokenType = "bearer",
                ExpiresIn = appSettingsOAuth.AccessTokenExpires,
                RefreshToken = refreshToken
            };
        }
    }
}