using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Deliverers.Application.Abstractions.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MotorcycleRental.Deliverers.Presentation.Implementations;

public class UsersService(HttpClient httpClient) : IUsersService, IDisposable
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Result<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
    {
        using var response = await _httpClient.PostAsJsonAsync("/users", request, GetSerializerOptions());

        if (response.IsSuccessStatusCode)
        {
            var createUserResponse = await response.Content.ReadFromJsonAsync<CreateUserResponse>(GetSerializerOptions());

            return createUserResponse!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();

        return new StatusCodeError((int)response.StatusCode, errorResponse!.Code, errorResponse.Error);
    }

    private static JsonSerializerOptions GetSerializerOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        options.Converters.Add(new JsonStringEnumConverter<PermissionType>());

        return options;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClient.Dispose();
    }
}
