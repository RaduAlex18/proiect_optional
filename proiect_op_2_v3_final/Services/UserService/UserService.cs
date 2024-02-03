using AutoMapper;
using proiect_op_2_v3_final.Helpers.JwtUtil;
using proiect_op_2_v3_final.Models;
using proiect_op_2_v3_final.Models.DTOs;
using proiect_op_2_v3_final.Models.Enums;
using proiect_op_2_v3_final.Repositories.UserRepository;
using proiect_op_2_v3_final.Services.UserService;
using BCryptNet = BCrypt.Net.BCrypt;

namespace proiect_op_2_v3_final.Services.UserService
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }


        public async Task<List<UserRegisterDTO>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserRegisterDTO>>(userList);
        }

        public UserRegisterDTO GetUserByNickname(string nickname)
        {
            var user = _userRepository.FindByNickname(nickname);
            return _mapper.Map<UserRegisterDTO>(user);
        }

        public User GetById(Guid id)
        {
            return _userRepository.FindById(id);
        }

        public async Task<UserLoginResponse> Login(UserLoginDTO userDTO)
        {
            var user = _userRepository.FindByNickname(userDTO.Nickname);

            if (user == null || !BCryptNet.Verify(userDTO.Password, user.Password))
            {
                return null; 
            }
            if (user == null) return null;

            var token = _jwtUtils.GenerateJwtToken(user);
            return new UserLoginResponse(user, token);
        }

        public async Task<bool> Register(UserRegisterDTO userRegisterDTO, Role userRole)
        {
            var userToCreate = new User
            {
                Nickname = userRegisterDTO.Nickname,
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                Role = userRole,
                Password = BCryptNet.HashPassword(userRegisterDTO.Password)
            };

            _userRepository.Create(userToCreate);
            return await _userRepository.SaveAsync();
        }
    }
}