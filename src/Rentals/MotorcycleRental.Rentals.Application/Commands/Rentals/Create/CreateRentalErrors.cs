using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.Create;

public static class CreateRentalErrors
{
    public static readonly UnprocessableEntityError DelivererInvalidCnhType = new("CreateRental.DelivererId.CnhType", "O usuário precisa possuir CNH do tipo A para poder locar uma moto.");

    public static readonly UnprocessableEntityError RentalMotorcycleExists = new("CreateRental.MotorcycleId.Exists", "Já existe uma locação para essa moto.");

    public static readonly UnprocessableEntityError RentalDelivererExists = new("CreateRental.DelivererId.Exists", "Já existe uma locação para esse entregador.");

    public static readonly NotFoundError RentalTypeNotFound = new("CreateRental.RentalTypeId.NotFound", "Não foi possível encontrar um tipo de locação com esse id.");
}
