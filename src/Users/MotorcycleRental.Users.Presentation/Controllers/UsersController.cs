using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Users.Application.Commands.Users.Create;

namespace MotorcycleRental.Users.Presentation.Controllers;

public class UsersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserCommand createUserCommand,
        [FromServices] IMediator mediator)
    {
        var userResult = await mediator.Send(createUserCommand);

        return ToActionResult(userResult);
    }
}
