using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Results;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Core.Presentation;

public static class DependencyInjections
{
    public static IServiceCollection AddPresentationCore(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<PermissionType>());
        });

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddExceptionHandler<ExceptionHandler>();

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultConventionHandler>();

        return services;
    }
}
