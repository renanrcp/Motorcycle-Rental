using MotorcycleRental.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;

namespace MotorcycleRental.Motorcycles.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddInfrastructureCore();
        services.AddApplicationContext<MotorcyclesDbContext>();

        return services;
    }
}
