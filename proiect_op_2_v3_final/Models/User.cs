using proiect_op_2_v3_final.Models.Base;
using proiect_op_2_v3_final.Models.Enums;
using System.Text.Json.Serialization;

namespace proiect_op_2_v3_final.Models
{
    public class User : BaseE
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Nickname { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
