using proiect_op_2_v3_final.Models;

namespace proiect_op_2_v3_final.Helpers.JwtUtil
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? GetUserId(string? token);
    }
}
