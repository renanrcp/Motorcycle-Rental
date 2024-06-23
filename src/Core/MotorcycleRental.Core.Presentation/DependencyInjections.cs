using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Results;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Core.Presentation;

public static class DependencyInjections
{
    public static IServiceCollection AddPresentationCore(this IServiceCollection services)
    {
        services.AddControllers(o =>
        {
            o.Filters.Add<ResponseConventionFilter>(int.MaxValue);
        })
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<PermissionType>());
        });

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddExceptionHandler<ExceptionHandler>();

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultConventionHandler>();

        return services;
    }

    public static WebApplicationBuilder AddEnvFileIfDevelopment(this WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment())
        {
            return builder;
        }

        var path = Path.Join("..", "..", "..", ".env.development");

        if (!File.Exists(path))
            return builder;

        foreach (var line in File.ReadAllLines(path))
        {
            var part = line.Split(
                '=',
                StringSplitOptions.RemoveEmptyEntries);

            if (part.Length < 2)
                continue;

            Environment.SetEnvironmentVariable(part[0], string.Join("=", part.Skip(1)));
            builder.Configuration[part[0].Replace("__", ":")] = string.Join("=", part.Skip(1));
        }

        return builder;
    }
}
