using MotorcycleRental.Deliverers.Domain.Entities;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;

public class CreateDelivererResponse
{
    public required int Id { get; init; }

    public required string Email { get; init; }

    public required string Name { get; init; }

    public required string Cnpj { get; init; }

    public required string Cnh { get; init; }

    public required CnhType CnhType { get; init; }

    public required DateOnly BirthDate { get; init; }
}
