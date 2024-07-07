using Barber.API.Filters;
using Barber.API.Helper;
using Barber.Application.Services;
using Barber.Domain.Interfaces;
using Barber.Infrastructure.IoC.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider());
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
        
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Barber", Version = "v1" });
    c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date-time", Example = new OpenApiString("dd/MM/yyyy HH:mm") });
    c.OperationFilter<CustomDateTimeFormatOperationFilter>();

});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddAntiforgery(options =>
{
    options.SuppressXFrameOptionsHeader = true;
});
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
   
});
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CreateRoles(app);
app.UseHttpsRedirection();

app.UseHsts();

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
        service.SeedUsers();
    }
}
