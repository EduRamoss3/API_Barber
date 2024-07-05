using Microsoft.AspNetCore.Mvc.Filters;

namespace Barber.API.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger; 
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExectuing");
            _logger.LogInformation("### #################");
            _logger.LogInformation($"StatusCode : {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExectuing");
            _logger.LogInformation("### #################");
            _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
           
        }
    }
}
