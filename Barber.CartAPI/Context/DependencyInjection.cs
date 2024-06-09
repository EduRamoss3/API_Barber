using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Barber.CartAPI.Context
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString: configuration.GetConnectionString("DefaultConnection")
            , serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddAuthentication();
            return services;
        }
    }
}
