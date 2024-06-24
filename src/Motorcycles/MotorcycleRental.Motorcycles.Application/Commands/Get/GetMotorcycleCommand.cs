
using MotorcycleRental.Core.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Motorcycles.Application.Commands.Get;

public class GetMotorcycleCommand : ICommand<GetMotorcycleResponse>
{
    [Required]
    public required int MotorcycleId { get; init; }
}
