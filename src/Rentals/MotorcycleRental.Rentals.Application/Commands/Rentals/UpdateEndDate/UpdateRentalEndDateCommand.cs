using MotorcycleRental.Core.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.UpdateEndDate;

public class UpdateRentalEndDateCommand : ICommand<UpdateRentalEndDateResponse>
{
    [Required]
    public required DateTime EndDate { get; init; }
}
