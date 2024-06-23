using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Presentation.Controllers;
using MotorcycleRental.Users.Application.Commands.Users.Create;
using MotorcycleRental.Users.Application.Commands.Users.Login;

namespace MotorcycleRental.Users.Presentation.Controllers;

public class UsersController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
    {
        var userResult = await _mediator.Send(createUserCommand);

        return ToActionResult(userResult);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var userResult = await _mediator.Send(loginUserCommand);

        return ToActionResult(userResult);
    }
}
