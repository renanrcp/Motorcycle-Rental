using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Motorcycles.Application.Commands.Get;

public static class GetMotorcycleErrors
{
    public static readonly NotFoundError MotorcycleNotFound = new("GetMotorcycle.Id.NotFound", "Não foi possível encontrar uma moto como esse id.");
}
