
using Barber.Application.Interfaces;
using Barber.Application.Mappings;
using Barber.Application.Services;
using Barber.Domain;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.Data.Context;
using Barber.Infrastructure.Data.Identitys;
using Barber.Infrastructure.Data.Repository;
using Barber.Infrastructure.Data.Repository.UnityOfWork;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;

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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddMediatR(myhandlers);
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddAutoMapper(typeof(CQRSToDTOMappingProfile));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Task>>();
                logger.LogError("Authentication failed.", context.Exception);
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "Unauthorized access" });
                return context.Response.WriteAsync(result);
            }
        };
    });
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Igual ao tempo de expiração do token JWT
                options.SlidingExpiration = true; // Renova a expiração com cada solicitação
            });
            services.AddAuthorization();

            return services;
        }
    }
}