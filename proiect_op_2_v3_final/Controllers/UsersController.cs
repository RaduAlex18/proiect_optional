using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using proiect_op_2_v3_final.Services.UserService;

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
    }
}