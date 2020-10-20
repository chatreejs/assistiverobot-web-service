using System;

namespace AssistiveRobot.Web.Service.Domains
{
    public partial class UserToken
    {
        public long UserId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
        public string RefreshToken { get; set; }
        public string FcmToken { get; set; }
        public string Nonce { get; set; }
        public string CheckSum { get; set; }
    } 
}