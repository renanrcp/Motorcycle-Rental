namespace MotorcycleRental.Rentals.Application.Commands.Rentals.Create;

public class CreateRentalResponse
{
    public required int MotorcycleId { get; init; }

    public required int DelivererId { get; init; }

    public required int RentalTypeId { get; init; }

    public required DateTime StartDate { get; init; }

    public required DateTime ExpectedEndDate { get; init; }
}
