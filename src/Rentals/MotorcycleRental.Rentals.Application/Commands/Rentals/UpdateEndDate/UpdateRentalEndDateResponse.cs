namespace MotorcycleRental.Rentals.Application.Commands.Rentals.UpdateEndDate;

public class UpdateRentalEndDateResponse
{
    public required int MotorcycleId { get; init; }

    public required int DelivererId { get; init; }

    public required int RentalTypeId { get; init; }

    public required DateTime StartDate { get; init; }

    public required DateTime ExpectedEndDate { get; init; }

    public required DateTime EndDate { get; set; }

    public required decimal TotalCost { get; init; }
}

