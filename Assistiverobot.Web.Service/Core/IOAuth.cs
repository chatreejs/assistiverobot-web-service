namespace AssistiveRobot.Web.Service.Core
{
    public interface IOAuth
    {
        int AccessTokenExpires { get; set; }
        int RefreshTokenExpires { get; set; }
        int AuthorizationCodeExpires { get; set; }
        string Issuer { get; set; }
        string SecretKey { get; set; }
        bool MultiRefreshToken { get; set; }
        string SecretSalt { get; }
    }
}