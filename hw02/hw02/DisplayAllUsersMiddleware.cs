using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace hw02
{
    public class DisplayAllUsersMiddleware
    {
        private readonly RequestDelegate _next;

        public DisplayAllUsersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/")
            {
                var users = User.GetAllUsers();
                if (users.Count == 0)
                {
                    await httpContext.Response.WriteAsync("No users");
                }
                else
                {
                    var responseText = new StringBuilder("User list:\n");
                    foreach (var user in users)
                    {
                        responseText.AppendLine($"Name: {user.Name}, login: {user.Login}");
                    }
                    await httpContext.Response.WriteAsync(responseText.ToString());
                }
                return;
            }

            await _next(httpContext);
        }
    }

    public static class DisplayAllUsersMiddlewareExtensions
    {
        public static IApplicationBuilder UseDisplayAllUsersMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DisplayAllUsersMiddleware>();
        }
    }
}