using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Users.Application.Responses.Users.Login;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Users.Application.Commands.Users.Login;

public class LoginUserCommand : ICommand<LoginResponse>
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
