using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Motorcycles.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Motorcycles.Application.Commands.Create;

public class CreateMotorycleCommand : ICommand<CreateMotorcycleResponse>
{
    [Required]
    [LicensePlate]
    public required string LicensePlate { get; init; }

    [Required]
    public required string Model { get; init; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int Year { get; init; }
}
