
using Barber.Application.Mappings;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Barber.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Infrastructure.IoC.DependencyInjection
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString: configuration.GetConnectionString("DefaultConnection")
            , serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

            services.AddScoped<IBarberRepository, BarberRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ISchedulesRepository, SchedulesRepository>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddAutoMapper(typeof(CQRSToDTOMappingProfile));
            services.AddAuthentication();
            return services;
        }
    }
}