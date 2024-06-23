using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Motorcycles.Application.Commands.Update;

public static class UpdateMotorcycleErrors
{
    public static readonly NotFoundError MotorcycleNotFound = new("UpdateMotorcycleLicensePlate.Id.NotFound", "Não foi possível encontrar uma moto como esse id.");

    public static readonly UnprocessableEntityError MotorcycleExists = new("UpdateMotorcycleLicensePlate.LicensePlate.Duplicated", "Já existe uma moto com essa placa.");
}
