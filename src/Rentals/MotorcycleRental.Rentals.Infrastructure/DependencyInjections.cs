using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Core.Infrastructure;
using MotorcycleRental.Rentals.Infrastructure.Contexts;

namespace MotorcycleRental.Rentals.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddInfrastructureCore();
        services.AddApplicationContext<RentalsDbContext>();

        return services;
    }
}
