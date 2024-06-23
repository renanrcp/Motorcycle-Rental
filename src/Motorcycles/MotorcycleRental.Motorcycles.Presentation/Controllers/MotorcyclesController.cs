using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Core.Presentation.Requirements;
using MotorcycleRental.Motorcycles.Application.Commands.Create;
using MotorcycleRental.Motorcycles.Application.Commands.List;

namespace MotorcycleRental.Motorcycles.Presentation.Controllers;

public class MotorcyclesController(IMediator mediator) : AuthorizeController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [RequirePermissions(PermissionType.CanListMotorcycles)]
    public async Task<IActionResult> List([FromQuery] ListMotorcycleCommand listMotorcycleCommand)
    {
        var listMotorcycleCommandResult = await _mediator.Send(listMotorcycleCommand);

        return ToActionResult(listMotorcycleCommandResult);
    }

    [HttpPost]
    [RequirePermissions(PermissionType.CanCreateMotorcycle)]
    public async Task<IActionResult> Create([FromBody] CreateMotorycleCommand createMotorycleCommand)
    {
        var createMotorcycleCommandResult = await _mediator.Send(createMotorycleCommand);

        return ToActionResult(createMotorcycleCommandResult);
    }

}
