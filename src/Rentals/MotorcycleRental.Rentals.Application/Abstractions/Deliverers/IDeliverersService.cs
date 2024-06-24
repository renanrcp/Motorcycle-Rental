using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Rentals.Application.Abstractions.Deliverers;

public interface IDeliverersService
{
    Task<Result<GetCurrentDelivererResponse>> GetCurrentDelivererAsync();
}
