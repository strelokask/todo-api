using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Todo.Infra
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation($"Processing {context.Request.Method} {context.Request.Path}");
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal exception has occurred");
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("An internal exception has occurred");
            }
            finally
            {
                _logger.LogInformation($"Processed {context.Request.Method} {context.Request.Path}");
            }
        }
    }
}
