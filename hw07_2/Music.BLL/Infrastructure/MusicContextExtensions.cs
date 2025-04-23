using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Music.DAL.EF;

namespace Music.BLL.Infrastructure
{
    public static class MusicContextExtensions
    {
        public static void AddMusicContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));
        }
    }
}
