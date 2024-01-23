namespace proiect_op_2_v3_final.Models.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public string Email { get; set; }
        public string Nickname { get; set; }

        //public string Password { get; set; }
    }
}
