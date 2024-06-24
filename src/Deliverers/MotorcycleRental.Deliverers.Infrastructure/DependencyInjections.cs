using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Core.Infrastructure;
using MotorcycleRental.Deliverers.Infrastructure.Contexts;

namespace MotorcycleRental.Deliverers.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddInfrastructureCore();
        services.AddApplicationContext<DeliverersDbContext>();

        return services;
    }
}
