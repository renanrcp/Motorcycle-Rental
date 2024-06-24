using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Motorcycles.Application.Abstractions.Rentals;

public interface IRentalsService
{
    Task<Result<GetRentalByMotocycleResponse>> GetRentalByMotorcycleAsync(GetRentalByMotorcycleRequest request);
}

