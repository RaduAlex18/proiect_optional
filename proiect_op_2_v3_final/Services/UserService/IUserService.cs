using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Models.DTOs;
using proiect_op_2_v3_final.Models.Enums;

namespace proiect_op_2_v3_final.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserRegisterDTO>> GetAllUsers();

        UserRegisterDTO GetUserByNickname(string nickname);

        Task<UserLoginResponse> Login(UserLoginDTO user);
        User GetById(Guid id);

        Task<bool> Register(UserRegisterDTO userRegisterDTO, Role userRole);
    }
}
