using Barber.Domain.Interfaces;
using Barber.Infrastructure.IoC.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CreateRoles(app);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void CreateRoles(WebApplication api)
{
    var scopedFactory = api.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedRolesInitial>();
        service.SeedRoles();
    }
}
