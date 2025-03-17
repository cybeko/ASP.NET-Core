using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace hw02
{
    public class RegisterMiddleware
    {
        private readonly RequestDelegate _next;

        public RegisterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var query = httpContext.Request.Query;
            if (httpContext.Request.Path == "/register")
            {
                if (query.ContainsKey("name") && query.ContainsKey("login") && query.ContainsKey("password"))
                {
                    var user = new User
                    {
                        Name = query["name"],
                        Login = query["login"],
                        Password = query["password"]
                    };

                    User.SaveUser(user);
                    await httpContext.Response.WriteAsync("Registered successfully");
                    return;
                }
                else
                {
                    await httpContext.Response.WriteAsync("Error: enter name, login and password");
                    return;
                }
            }

            await _next(httpContext);
        }
    }

    public static class RegisterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRegisterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RegisterMiddleware>();
        }
    }
}