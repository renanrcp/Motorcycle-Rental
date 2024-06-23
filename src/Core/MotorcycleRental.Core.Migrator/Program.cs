using MotorcycleRental.Core.Migrator.Contexts;
using MotorcycleRental.Core.Infrastructure;
using MotorcycleRental.Core.Migrator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextInternal<MigrationDbContext>();
builder.Services.AddHostedService<MigrationService>();

var app = builder.Build();

app.Run();
