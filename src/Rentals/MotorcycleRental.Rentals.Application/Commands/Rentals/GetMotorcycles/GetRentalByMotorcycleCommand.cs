using MotorcycleRental.Core.Application.Abstractions;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.GetMotorcycles;

public class GetRentalByMotorcycleCommand : ICommand<GetRentalByMotorcycleResponse>
{
    public required int MotorcycleId { get; init; }
}
