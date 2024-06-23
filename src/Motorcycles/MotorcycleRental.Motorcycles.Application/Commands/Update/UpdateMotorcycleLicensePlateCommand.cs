using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Motorcycles.Application.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Motorcycles.Application.Commands.Update;

public class UpdateMotorcycleLicensePlateCommand : ICommand<UpdateMotorcycleLicensePlateResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    [LicensePlate]
    public required string LicensePlate { get; init; }
}
