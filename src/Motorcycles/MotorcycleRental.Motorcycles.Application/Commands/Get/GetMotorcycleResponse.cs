namespace MotorcycleRental.Motorcycles.Application.Commands.Get;

public class GetMotorcycleResponse
{
    public required int Id { get; init; }

    public required string LicensePlate { get; init; }

    public required string Model { get; init; }

    public required int Year { get; init; }
}
