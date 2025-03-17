using hw01;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseFirstMiddleware();
app.UseSecondMiddleware();
app.UseThirdMiddleware();
app.Run();
