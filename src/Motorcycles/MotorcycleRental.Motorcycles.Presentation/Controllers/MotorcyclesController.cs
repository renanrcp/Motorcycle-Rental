using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Core.Presentation.Requirements;
using MotorcycleRental.Motorcycles.Application.Commands.Create;
using MotorcycleRental.Motorcycles.Application.Commands.Get;
using MotorcycleRental.Motorcycles.Application.Commands.List;
using MotorcycleRental.Motorcycles.Application.Commands.Update;

namespace MotorcycleRental.Motorcycles.Presentation.Controllers;

public class MotorcyclesController(IMediator mediator) : AuthorizeController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var getMotorcycleCommand = new GetMotorcycleCommand
        {
            MotorcycleId = id,
        };

        var getMotorcycleCommandResult = await _mediator.Send(getMotorcycleCommand);

        return ToActionResult(getMotorcycleCommandResult);
    }

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

    [HttpPatch("{id}")]
    [RequirePermissions(PermissionType.CanUpdateMotorcycles)]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] UpdateMotorcycleLicensePlateCommand updateMotorcycleLicensePlateCommand)
    {
        updateMotorcycleLicensePlateCommand.Id = id;

        var updateMotorcycleLicensePlateCommandResult = await _mediator.Send(updateMotorcycleLicensePlateCommand);

        return ToActionResult(updateMotorcycleLicensePlateCommandResult);
    }
}
