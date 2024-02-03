using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proiect_op_2_v3_final.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using proiect_op_2_v3_final.Models.Enums;
using proiect_op_2_v3_final.Models.DTOs;

namespace proiect_op_2_v3_final.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUserByUserName([FromBody] string username)
        {
            return Ok(_userService.GetUserByNickname(username));
        }


        //login

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Test(UserLoginDTO userLoginDto)
        {
            return Ok("Users");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDto)
        {
            var response = await _userService.Login(userLoginDto);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserRegisterDTO userRegisterDto)
        {
            var response = await _userService.Register(userRegisterDto, Models.Enums.Role.User);

            if (response == false)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(UserRegisterDTO userRegisterDto)
        {
            var response = await _userService.Register(userRegisterDto, Models.Enums.Role.Admin);

            if (response == false)
            {
                return BadRequest();
            }

            return Ok(response);
        }


        [Authorize]
        [HttpGet("check-auth-without-role")]
        public IActionResult GetText()
        {
            return Ok(new { Message = "Account is logged in" });
        }

        [Authorize(Roles = nameof(Role.User))]
        [HttpGet("check-auth-user")]
        public IActionResult GetTextUser()
        {
            return Ok(new { Message = "User is logged in" });
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("check-auth-admin")]
        public IActionResult GetTextAdmin()
        {
            return Ok(new { Message = "Admin is logged in" });
        }

        [Authorize(Roles = nameof(Role.Admin) + "," + nameof(Role.User))]
        [HttpGet("check-auth-admin-and-user")]
        public IActionResult GetTextAdminUser()
        {
            return Ok(new { Message = "Account is user or admin" });
        }

    }
}