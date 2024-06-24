using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Rentals.Application.Abstractions.Motorcycles;

public interface IMotorcyclesService
{
    Task<Result<GetMotorcycleResponse>> GetMotorcycleAsync(GetMotorcycleRequest request);
}
