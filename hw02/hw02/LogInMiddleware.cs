using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace hw02
{
    public class LogInMiddleware
    {
        private readonly RequestDelegate _next;

        public LogInMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var query = httpContext.Request.Query;
            if (httpContext.Request.Path == "/login")
            {
                if (query.ContainsKey("login") && query.ContainsKey("password"))
                {
                    string login = query["login"];
                    string password = query["password"];

                    if (User.ValidateUser(login, password))
                    {
                        await httpContext.Response.WriteAsync("Log in successful");
                    }
                    else
                    {
                        await httpContext.Response.WriteAsync("Error: incorrect credentials.");
                    }
                    return;
                }
                else
                {
                    await httpContext.Response.WriteAsync("Error: enter login and password");
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class LogInMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogInMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogInMiddleware>();
        }
    }
}