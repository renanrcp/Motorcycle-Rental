using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Results;

namespace MotorcycleRental.Core.Presentation;

public static class DependencyInjections
{
    public static IServiceCollection AddPresentationCore(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddExceptionHandler<ExceptionHandler>();

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultConventionHandler>();

        return services;
    }
}
