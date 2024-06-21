using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MotorcycleRental.Core.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, Assembly applicationAssembly)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));

        return services;
    }
}
