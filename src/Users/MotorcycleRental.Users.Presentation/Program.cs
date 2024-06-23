using MotorcycleRental.Core.Application;
using MotorcycleRental.Users.Application.Commands.Users.Create;
using MotorcycleRental.Users.Infrastructure;
using MotorcycleRental.Core.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentationCore();

builder.Services.AddApplicationCore(typeof(CreateUserCommand).Assembly);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UsePresentationCoreMiddlewares();

app.Run();
