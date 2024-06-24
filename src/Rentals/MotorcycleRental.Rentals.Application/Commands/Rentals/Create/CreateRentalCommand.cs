using MotorcycleRental.Core.Application.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.Create;

public class CreateRentalCommand : ICommand<CreateRentalResponse>
{
    [Required]
    public required int MotorcycleId { get; init; }

    [Required]
    public required int RentalTypeId { get; init; }
}
