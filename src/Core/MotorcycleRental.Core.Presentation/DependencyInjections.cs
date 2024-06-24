using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MotorcycleRental.Core.Application;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Results;
using Serilog;
using Serilog.Events;
using System.Text;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Core.Presentation;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddPresentationCore(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(LogEventLevel.Debug)
            .WriteTo.File(Path.Join("logs", "log.txt"),
                LogEventLevel.Warning,
                rollingInterval: RollingInterval.Day));

        builder.AddEnvFileIfDevelopment();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers(o =>
        {
            o.Filters.Add<ResponseConventionFilter>(int.MaxValue);
        })
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        builder.Services.AddExceptionHandler<ExceptionHandler>();

        builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultConventionHandler>();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = ApplicationCoreConstants.Issuer,
                ValidAudience = ApplicationCoreConstants.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApplicationCoreConstants.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
            };
        });

        builder.Services.AddScoped<IAuthorizationHandler, RequirePermissionsAuthorizationHandler>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
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

    public static WebApplication UsePresentationCoreMiddlewares(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseExceptionHandler(o => { });

        var applicationName = app.Services.GetRequiredService<IHostEnvironment>().ApplicationName
                                                .Replace(".", string.Empty)
                                                .Replace("MotorcycleRental", string.Empty)
                                                .Replace("Presentation", string.Empty)
                                                .ToLower();

        app.UseSwagger(c =>
        {
            c.RouteTemplate = applicationName + "/swagger/{documentName}/swagger.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/{applicationName}/swagger/v1/swagger.json", $"{applicationName} API");
            c.RoutePrefix = $"{applicationName}/swagger";
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers()
           .WithOpenApi();

        return app;
    }
}
