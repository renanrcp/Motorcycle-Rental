using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.UpdateEndDate;

public static class UpdateRentalEndDateErrors
{
    public static readonly NotFoundError RentalNotFound = new("UpdateRentalEndDate.RentalId.NotFound", "Não foi possível encontrar uma locação para esse usuário.");
}
