using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Core.Infrastructure;
using MotorcycleRental.Users.Infrastructure.Contexts;

namespace MotorcycleRental.Users.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddInfrastructureCore();
        services.AddApplicationContext<UsersDbContext>();

        return services;
    }

}
