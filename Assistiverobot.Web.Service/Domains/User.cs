using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssistiveRobot.Web.Service.Domains
{
    [Table("Users")]
    public partial class User
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
