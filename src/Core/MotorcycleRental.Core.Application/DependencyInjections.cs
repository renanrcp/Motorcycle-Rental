using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Application.Implementations;
using MotorcycleRental.Core.Domain.Abstractions;
using System.Reflection;

namespace MotorcycleRental.Core.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, Assembly applicationAssembly)
    {
        services.AddSingleton<IDomainEventSaver, EventSaver>();
        services.AddSingleton<IApplicationEventSaver, EventSaver>();

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));

        return services;
    }
}
