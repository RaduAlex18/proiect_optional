using proiect_op_2_v3_final.Helpers.JwtUtil;
using proiect_op_2_v3_final.Services.UserService;

namespace proiect_op_2_v3_final.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtil)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtil.GetUserId(token);
            if (userId != null)
            {
                context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
