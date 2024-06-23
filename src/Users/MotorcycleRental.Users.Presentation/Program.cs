using MotorcycleRental.Core.Application;
using MotorcycleRental.Users.Application.Commands.Users.Create;
using MotorcycleRental.Users.Infrastructure;
using MotorcycleRental.Core.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddEnvFileIfDevelopment();

builder.Services.AddApplicationCore(typeof(CreateUserCommand).Assembly);
builder.Services.AddInfrastructure();
builder.Services.AddPresentationCore();

var app = builder.Build();

app.UseExceptionHandler(o => { });

app.MapControllers();

app.Run();
