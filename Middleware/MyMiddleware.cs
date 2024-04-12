using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VersionsCRUD.Common;

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
                //Get the request path
                var path = JsonConvert.SerializeObject(httpContext.Request.Path.Value ?? "");
                
                //Get the headers
                var headers = JsonConvert.SerializeObject(httpContext.Request.Headers);

                //Get the Request Body - to be continued
                //Stream stream = httpContext.Request.Body;
                //httpContext.Response.Body = new MemoryStream();
                //string _originalContent = new StreamReader(stream).ReadToEnd();

                var body = "";
                _logger.LogInformation($"new hit : {path}, headers : {headers}, body : {body}");

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
                //Get the Response Body - to be continued
                //_logger.LogInformation($"response : {httpContext.Response.Body}");

            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError($"An error occurred in MyMiddleware: {ex}");
                CommonResp errorResp = new();
                errorResp.code = 1;
                errorResp.message = "Something went wrong. Please try again later ! ";

                // Serialize error response to JSON
                var jsonResponse = JsonConvert.SerializeObject(errorResp);

                // Set content type header
                httpContext.Response.ContentType = "application/json";

                // Write JSON response to the response stream
                await httpContext.Response.WriteAsync(jsonResponse);

                //Another way
                //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //await httpContext.Response.WriteAsync("An unexpected error occurred. Please try again later.");
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
