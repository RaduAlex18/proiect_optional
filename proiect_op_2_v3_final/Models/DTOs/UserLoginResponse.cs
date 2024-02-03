namespace proiect_op_2_v3_final.Models.DTOs
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Token { get; set; }


        public UserLoginResponse(User user, string token)
        {
            Id = user.Id;
            Nickname = user.Nickname;
            Token = token;
        }
    }
}
