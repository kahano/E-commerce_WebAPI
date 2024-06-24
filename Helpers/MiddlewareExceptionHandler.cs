using System.Net;
using System.Text.Json;

namespace E_commercial_Web_RESTAPI.Helpers
{
    public class MiddlewareExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExceptionHandler> _logger;
        private readonly IHostEnvironment _enviroment;

        public MiddlewareExceptionHandler(
            RequestDelegate next,
            ILogger<MiddlewareExceptionHandler> logger,
            IHostEnvironment enviroment)
        {
            _next = next;
            _logger = logger;
            _enviroment = enviroment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _enviroment.IsDevelopment() ?
                    new ApiResponse {  Message = ex.Message, StatusCode = (int)HttpStatusCode.InternalServerError }:
                    new ApiResponse { StatusCode = (int)HttpStatusCode.InternalServerError };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
