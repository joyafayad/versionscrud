﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace VersionsCRUD.Models
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public MyMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyMiddleware");
        }
        //public async Task Invoke(HttpContext httpContext)
        //{
        //    _logger.LogInformation("MyMiddleware executing..");
        //    await _next(httpContext); // calling next middleware
        //}
        public async Task Invoke(HttpContext httpContext) // check  test
        {

            try
            {
                _logger.LogInformation("MyMiddleware executing..");
                await _next(httpContext); // calling next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in MyMiddleware.");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
    
}
