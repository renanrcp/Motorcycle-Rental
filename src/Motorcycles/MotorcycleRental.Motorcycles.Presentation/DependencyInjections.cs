using MotorcycleRental.Core.Presentation;
using MotorcycleRental.Core.Presentation.DelegatingHandlers;
using MotorcycleRental.Motorcycles.Application.Abstractions.Rentals;
using MotorcycleRental.Motorcycles.Presentation.Implementations;
using Polly;

namespace MotorcycleRental.Motorcycles.Presentation;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.AddPresentationCore();

        builder.Services.AddTransient<AuthenticationDelegatingHandler>();

        builder.Services.AddHttpClient<IRentalsService, RentalsService>((sp, options) =>
        {
            var enviroment = sp.GetRequiredService<IHostEnvironment>();

            if (enviroment.IsDevelopment())
            {
                options.BaseAddress = new Uri("http://localhost:5104/");
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
