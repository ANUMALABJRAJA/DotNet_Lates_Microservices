using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eCommerece.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
          
            try{
                await _next(context);  // Call the next middleware
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                
                if(ex.InnerException is not null)
                {
                    _logger.LogError($"{ex.InnerException.GetType()} : {ex.InnerException.Message}");
                }


                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new {Message = ex.Message, Type = ex.GetType().ToString()});
            }
             
            
            
        }
    }

    public static class ExceptionHandlingMiddlewareExtension{
        public static IApplicationBuilder useExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
                return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
