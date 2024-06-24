using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Rentals.Application.Abstractions.Deliverers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Rentals.Presentation.Implementations;

public class DeliverersService(HttpClient httpClient) : IDeliverersService, IDisposable
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Result<GetCurrentDelivererResponse>> GetCurrentDelivererAsync()
    {
        using var response = await _httpClient.GetAsync("/deliverers");

        if (response.IsSuccessStatusCode)
        {
            var getCurrentDelivererResponse = await response.Content.ReadFromJsonAsync<GetCurrentDelivererResponse>(GetSerializerOptions());

            return getCurrentDelivererResponse!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        return new StatusCodeError((int)response.StatusCode, errorResponse!.Code, errorResponse!.Error);
    }

    private static JsonSerializerOptions GetSerializerOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _httpClient.Dispose();
    }

}
