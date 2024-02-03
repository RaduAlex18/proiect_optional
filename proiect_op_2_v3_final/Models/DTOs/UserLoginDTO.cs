using System.ComponentModel.DataAnnotations;

namespace proiect_op_2_v3_final.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
