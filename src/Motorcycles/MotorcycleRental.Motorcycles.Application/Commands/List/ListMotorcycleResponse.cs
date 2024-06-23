namespace MotorcycleRental.Motorcycles.Application.Commands.List;

public class ListMotorcycleResponse
{
    public required int Id { get; init; }

    public required string LicensePlate { get; init; }

    public required string Model { get; init; }

    public required int Year { get; init; }
}
