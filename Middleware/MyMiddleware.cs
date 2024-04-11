using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace VersionsCRUD.Models

{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
       // private readonly ITokenService _tokenService;
        public MyMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyMiddleware");
           // _tokenService = tokenService;
        }
        //public async Task Invoke(HttpContext httpContext)
        //{
        //    _logger.LogInformation("MyMiddleware executing..");
        //    await _next(httpContext); // calling next middleware
        //}
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation("MyMiddleware executing..");

               // if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
               // {
                   // var token = authHeaderValue.ToString().Replace("Bearer ", "");
                    //var isValid = _tokenService.ValidateToken(token);

                   // if (!isValid)
                   //
                //else
                //{
                //    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //    await httpContext.Response.WriteAsync("Missing token");
                //    return;
                //}

                await _next(httpContext); 
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
