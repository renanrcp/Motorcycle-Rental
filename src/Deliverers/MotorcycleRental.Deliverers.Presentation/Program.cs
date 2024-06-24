using MotorcycleRental.Core.Application;
using MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;
using MotorcycleRental.Deliverers.Infrastructure;
using MotorcycleRental.Core.Presentation;
using MotorcycleRental.Deliverers.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

builder.Services.AddApplicationCore(typeof(CreateDelivererCommand).Assembly);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UsePresentationCoreMiddlewares();

app.Run();

