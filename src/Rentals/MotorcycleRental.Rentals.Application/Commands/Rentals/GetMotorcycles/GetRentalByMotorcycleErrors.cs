using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.GetMotorcycles;

public static class GetRentalByMotorcycleErrors
{
    public static readonly NotFoundError RentalNotFound = new("GetRentalByMotorcycle.MotorcycleId.NotFound", "Não foi possível encontrar uma locação para essa moto.");
}
