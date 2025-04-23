using Music.BLL.Infrastructure;
using Music.BLL.Interfaces;
using Music.BLL.Services;
using Music.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddMusicContext(connection);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5500") 
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<ISongService, SongService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors("AllowAll");
app.UseStaticFiles();
app.Run();
