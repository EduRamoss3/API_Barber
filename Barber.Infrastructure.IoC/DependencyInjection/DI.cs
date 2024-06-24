
using Barber.Application.Interfaces;
using Barber.Application.Mappings;
using Barber.Application.Services;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Barber.Infrastructure.Data.Identitys;
using Barber.Infrastructure.Data.Repository;
using MediatR;
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
            var myhandlers = AppDomain.CurrentDomain.Load("Barber.Application");


            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString: configuration.GetConnectionString("DefaultConnection")
            , serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


            services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
            });
            services.AddScoped<IBarberRepository, BarberRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ISchedulesRepository, SchedulesRepository>();
            services.AddScoped<IBarberService, BarberService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedRolesInitial, SeedRolesInitial>();

            services.AddMediatR(myhandlers);

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddAutoMapper(typeof(CQRSToDTOMappingProfile));
            services.AddAuthentication();
            
            return services;
        }
    }
}