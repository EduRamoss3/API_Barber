using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Barber.API.Filters
{
    public class CustomDateTimeFormatOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            var parameter = operation.Parameters.FirstOrDefault(p => p.Name == "dateSearch");
            if (parameter != null && parameter.Schema.Format == "date-time")
            {
                parameter.Schema.Example = new OpenApiString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
