using AutoMapper;
using proiect_op_2_v3_final.Models.DTOs;
using proiect_op_2_v3_final.Repositories.UserRepository;
using proiect_op_2_v3_final.Services.UserService;

namespace proiect_op_2_v3_final.Services.UserService
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<List<UserDTO>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDTO>>(userList);
        }

        public UserDTO GetUserByNickname(string nickname)
        {
            var user = _userRepository.FindByNickname(nickname);
            return _mapper.Map<UserDTO>(user);
        }
    }
}