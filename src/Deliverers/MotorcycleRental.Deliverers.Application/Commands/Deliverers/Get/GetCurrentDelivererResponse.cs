using MotorcycleRental.Deliverers.Domain.Entities;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Get;

public class GetCurrentDelivererResponse
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Cnpj { get; init; }

    public required string Cnh { get; init; }

    public required CnhType CnhType { get; init; }

    public required DateOnly BirthDate { get; init; }
}
