using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Rentals.Application.Abstractions.Deliverers;
using MotorcycleRental.Rentals.Application.Abstractions.Motorcycles;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Rentals.Presentation.Implementations;

public class MotorcyclesService(HttpClient httpClient) : IMotorcyclesService, IDisposable
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Result<GetMotorcycleResponse>> GetMotorcycleAsync(GetMotorcycleRequest request)
    {
        using var response = await _httpClient.GetAsync($"/motorcycles/{request.MotorcycleId}");

        if (response.IsSuccessStatusCode)
        {
            var getMotorcycleResponse = await response.Content.ReadFromJsonAsync<GetMotorcycleResponse>(GetSerializerOptions());

            return getMotorcycleResponse!;
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
