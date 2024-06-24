using MotorcycleRental.Core.Application;
using MotorcycleRental.Core.Presentation;
using MotorcycleRental.Motorcycles.Infrastructure;
using MotorcycleRental.Motorcycles.Application.Commands.Create;
using MotorcycleRental.Motorcycles.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

builder.Services.AddApplicationCore(typeof(CreateMotorycleCommand).Assembly);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UsePresentationCoreMiddlewares();

app.Run();
