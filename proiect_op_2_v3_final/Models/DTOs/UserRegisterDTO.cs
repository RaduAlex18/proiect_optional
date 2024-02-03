using System.ComponentModel.DataAnnotations;

namespace proiect_op_2_v3_final.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
