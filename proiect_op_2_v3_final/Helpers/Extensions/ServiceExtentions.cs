using proiect_op_2_v3_final.Helpers.JwtUtil;
using proiect_op_2_v3_final.Helpers.Seeders;
using proiect_op_2_v3_final.Repositories.UserRepository;
using proiect_op_2_v3_final.Services.UserService;

namespace proiect_op_2_v3_final.Helpers.Extensions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<UsersSeeder>();

            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IJwtUtils, JwtUtils>();

            return services;
        }
    }
}