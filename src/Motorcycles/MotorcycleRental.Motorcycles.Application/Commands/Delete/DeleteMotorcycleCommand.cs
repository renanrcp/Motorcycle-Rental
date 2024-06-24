using MotorcycleRental.Core.Application.Abstractions;

namespace MotorcycleRental.Motorcycles.Application.Commands.Delete;

public class DeleteMotorcycleCommand : ICommand
{
    public required int MotorycleId { get; init; }
}
