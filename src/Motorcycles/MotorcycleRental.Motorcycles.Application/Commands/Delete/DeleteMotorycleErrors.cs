using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Motorcycles.Application.Commands.Delete;

public static class DeleteMotorycleErrors
{
    public static readonly UnprocessableEntityError RentalExists = new("DeleteMotorcycle.MotorcycleId.RentalExists", "Existe uma locação associada a esse usuário.");
}
