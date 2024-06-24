using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Rentals.Application.Commands.Rentals.Create;
using MotorcycleRental.Rentals.Application.Commands.Rentals.Get;
using MotorcycleRental.Rentals.Application.Commands.Rentals.UpdateEndDate;

namespace MotorcycleRental.Rentals.Presentation.Controllers;

public class RentalsController(IMediator mediator) : AuthorizeController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var getRentalCommandResult = await _mediator.Send(new GetRentalCommand());

        return ToActionResult(getRentalCommandResult);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRentalCommand createRentalCommand)
    {
        var createRentalCommandResult = await _mediator.Send(createRentalCommand);

        return ToActionResult(createRentalCommandResult);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateEndDate([FromBody] UpdateRentalEndDateCommand updateRentalEndDateCommand)
    {
        var updateRentalEndDateCommandResult = await _mediator.Send(updateRentalEndDateCommand);

        return ToActionResult(updateRentalEndDateCommandResult);
    }
}
