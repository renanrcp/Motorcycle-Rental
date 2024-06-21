using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Core.Infrastructure.Contexts;

namespace MotorcycleRental.Core.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationContext<TContext>(this IServiceCollection services)
        where TContext : ApplicationContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            if (sp.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                _ = options.EnableDetailedErrors();
                _ = options.EnableSensitiveDataLogging();
            }

            _ = options.UseApplicationServiceProvider(sp);
            _ = options.UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>());

            _ = options.UseNpgsql(sp.GetRequiredService<IConfiguration>().GetConnectionString("Postgres"), postgresOptions =>
            {
            });

            options.UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
