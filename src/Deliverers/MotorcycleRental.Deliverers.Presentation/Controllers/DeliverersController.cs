using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;

namespace MotorcycleRental.Deliverers.Presentation.Controllers;

public class DeliverersController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDelivererCommand createDelivererCommand)
    {
        var createDelivererCommandResult = await _mediator.Send(createDelivererCommand);

        return ToActionResult(createDelivererCommandResult);
    }
}
