using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.Get;

public static class GetRentalErrors
{
    public static readonly NotFoundError RentalNotFound = new("GetRental.RentalId.NotFound", "Não foi possível encontrar uma locação para esse usuário.");
}
