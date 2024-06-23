using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Motorcycles.Application.Commands.Create;

public static class CreateMotorcycleErrors
{
    public static readonly UnprocessableEntityError MotorcycleExists = new("CreateMotorcycle.LicensePlate.Duplicated", "Já existe uma moto com essa placa.");
}
