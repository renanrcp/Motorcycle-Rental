using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MotorcycleRental.Core.Application;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Results;
using System.Text;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Core.Presentation;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddPresentationCore(this WebApplicationBuilder builder)
    {
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
        app.UseExceptionHandler(o => { });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
