using MotorcycleRental.Core.Presentation;
using MotorcycleRental.Deliverers.Application.Abstractions.Users;
using MotorcycleRental.Deliverers.Presentation.Implementations;
using Polly;

namespace MotorcycleRental.Deliverers.Presentation;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IUsersService, UsersService>((sp, options) =>
        {
            var enviroment = sp.GetRequiredService<IHostEnvironment>();

            if (enviroment.IsDevelopment())
            {
                options.BaseAddress = new Uri("http://localhost:5045/");
            }
            else
            {
                options.BaseAddress = new Uri("http://users:5000/");
            }

        })
        .AddTransientHttpErrorPolicy(policy =>
        {
            return policy.WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(5 * retryAttempt));
        });

        builder.AddPresentationCore();

        return builder;
    }
}
