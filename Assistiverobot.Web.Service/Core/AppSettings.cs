namespace AssistiveRobot.Web.Service.Core
{
    public class AppSettings
    {
        public OAuth OAuth { get; set; }
    }

    public class OAuth : IOAuth
    {
        public int AccessTokenExpires { get; set; }
        public int RefreshTokenExpires { get; set; }
        public int AuthorizationCodeExpires { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public bool MultiRefreshToken { get; set; }
        public string SecretSalt { get; } = "f08cbad583dd7d06744e9265f58af66f";
    }
}