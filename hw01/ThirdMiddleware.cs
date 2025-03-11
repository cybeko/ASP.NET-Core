using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace hw01
{
    public class ThirdMiddleware
    {
        private readonly RequestDelegate _next;

        public ThirdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Items.TryGetValue("Number", out var value) && value is int number)
            {
                await httpContext.Response.WriteAsync($"Number: {number}");
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Not a valid number");
            }
        }
    }
    public static class ThirdMiddlewareExtensions
    {
        public static IApplicationBuilder UseThirdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThirdMiddleware>();
        }
    }
}
