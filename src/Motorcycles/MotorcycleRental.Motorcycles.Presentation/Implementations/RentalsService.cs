using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Motorcycles.Application.Abstractions.Rentals;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Motorcycles.Presentation.Implementations;

public class RentalsService(HttpClient httpClient) : IRentalsService, IDisposable
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Result<GetRentalByMotocycleResponse>> GetRentalByMotorcycleAsync(GetRentalByMotorcycleRequest request)
    {
        using var response = await _httpClient.GetAsync($"/rentals/motorcycles/{request.MotorcycleId}");

        if (response.IsSuccessStatusCode)
        {
            var getMotorcycleResponse = await response.Content.ReadFromJsonAsync<GetRentalByMotocycleResponse>(GetSerializerOptions());

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
