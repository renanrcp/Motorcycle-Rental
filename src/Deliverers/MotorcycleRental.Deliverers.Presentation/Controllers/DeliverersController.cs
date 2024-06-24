using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.VOs;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;
using MotorcycleRental.Deliverers.Application.Commands.Deliverers.Get;
using MotorcycleRental.Deliverers.Application.Commands.Deliverers.UploadImage;

namespace MotorcycleRental.Deliverers.Presentation.Controllers;

public class DeliverersController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCurrent()
    {
        var userAuthVO = UserAuthVO.FromHttpContext(HttpContext);

        var getCurrentDelivererCommand = new GetCurrentDelivererCommand
        {
            DelivererId = userAuthVO.Id,
        };

        var getCurrentDelivererCommandResult = await _mediator.Send(getCurrentDelivererCommand);

        return ToActionResult(getCurrentDelivererCommandResult);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDelivererCommand createDelivererCommand)
    {
        var createDelivererCommandResult = await _mediator.Send(createDelivererCommand);

        return ToActionResult(createDelivererCommandResult);
    }

    [Authorize]
    [HttpPatch("image")]
    [RequestSizeLimit(20_971_520)] // 20MB
    public async Task<IActionResult> UploadImage([FromForm] UploadDelivererImageCommand uploadDelivererImageCommand)
    {
        var userAuthVO = UserAuthVO.FromHttpContext(HttpContext);

        uploadDelivererImageCommand.DelivererId = userAuthVO.Id;

        var uploadDelivererImageCommandResult = await _mediator.Send(uploadDelivererImageCommand);

        return ToActionResult(uploadDelivererImageCommandResult);
    }
}
