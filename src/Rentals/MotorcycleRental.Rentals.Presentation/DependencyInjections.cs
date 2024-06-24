using MotorcycleRental.Core.Presentation;
using MotorcycleRental.Rentals.Application.Abstractions.Deliverers;
using MotorcycleRental.Rentals.Application.Abstractions.Motorcycles;
using MotorcycleRental.Rentals.Presentation.DelegatingHandlers;
using MotorcycleRental.Rentals.Presentation.Implementations;
using Polly;

namespace MotorcycleRental.Rentals.Presentation;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.AddPresentationCore();

        builder.Services.AddTransient<AuthenticationDelegatingHandler>();

        builder.Services.AddHttpClient<IDeliverersService, DeliverersService>((sp, options) =>
        {
            var enviroment = sp.GetRequiredService<IHostEnvironment>();

            if (enviroment.IsDevelopment())
            {
                options.BaseAddress = new Uri("http://localhost:5099/");
            }
            else
            {
                options.BaseAddress = new Uri("http://deliverers/");
            }

        })
        .AddHttpMessageHandler<AuthenticationDelegatingHandler>()
        .AddTransientHttpErrorPolicy(policy =>
        {
            return policy.WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(5 * retryAttempt));
        });

        builder.Services.AddHttpClient<IMotorcyclesService, MotorcyclesService>((sp, options) =>
        {
            var enviroment = sp.GetRequiredService<IHostEnvironment>();

            if (enviroment.IsDevelopment())
            {
                options.BaseAddress = new Uri("http://localhost:5166/");
            }
            else
            {
                options.BaseAddress = new Uri("http://rentals/");
            }

        })
        .AddHttpMessageHandler<AuthenticationDelegatingHandler>()
        .AddTransientHttpErrorPolicy(policy =>
        {
            return policy.WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(5 * retryAttempt));
        });

        return builder;
    }
}
