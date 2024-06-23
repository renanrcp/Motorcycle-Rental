using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MotorcycleRental.Core.Infrastructure.Contexts;

namespace MotorcycleRental.Core.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationContext<TContext>(this IServiceCollection services)
        where TContext : ApplicationContext
    {
        return services.AddDbContextInternal<TContext>();
    }

    public static IServiceCollection AddDbContextInternal<TContext>(this IServiceCollection services)
        where TContext : DbContext
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

    public static IServiceCollection AddMongoDB(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            return new MongoClient(configuration.GetConnectionString("Mongo"));
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureCore(this IServiceCollection services)
    {
        services.AddMongoDB();

        return services;
    }


}
