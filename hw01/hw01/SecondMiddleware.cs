using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace hw01
{
    public class SecondMiddleware
    {
        private readonly RequestDelegate _next;

        public SecondMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Items.TryGetValue("Number", out var value) && value is int number)
            {
                if (number < 1 || number > 100000)
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Number must be between 1 and 100000");
                    return;
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid number");
                return;
            }

            await _next(httpContext);
        }
    }

    public static class SecondMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecondMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecondMiddleware>();
        }
    }
}
