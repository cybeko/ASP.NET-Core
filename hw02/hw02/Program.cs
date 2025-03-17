using hw02;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRegisterMiddleware();
app.UseLogInMiddleware();
app.UseDisplayAllUsersMiddleware();

app.Run();
