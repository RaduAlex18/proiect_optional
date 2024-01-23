using proiect_op_2_v3_final.Models.DTOs;

namespace proiect_op_2_v3_final.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();

        UserDTO GetUserByNickname(string nickname);
    }
}
