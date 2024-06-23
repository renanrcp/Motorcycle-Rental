using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.Abstractions;

namespace MotorcycleRental.Motorcycles.Application.Commands.List;

public class ListMotorcycleCommand : ICommand<IReadOnlyList<ListMotorcycleResponse>>
{
    [FromQuery]
    public string? LicensePlate { get; set; }
}
