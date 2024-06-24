using MotorcycleRental.Core.Application;
using MotorcycleRental.Core.Presentation;
using MotorcycleRental.Rentals.Application.Commands.Rentals.Create;
using MotorcycleRental.Rentals.Infrastructure;
using MotorcycleRental.Rentals.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

builder.Services.AddApplicationCore(typeof(CreateRentalCommand).Assembly);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UsePresentationCoreMiddlewares();

app.Run();
