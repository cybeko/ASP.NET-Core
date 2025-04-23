using AspNetCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*") 
               .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TeacherContext>(options =>
    options.UseSqlServer(connection)); 

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles(); 
app.UseHttpsRedirection();

app.MapControllers();  

app.Run();
