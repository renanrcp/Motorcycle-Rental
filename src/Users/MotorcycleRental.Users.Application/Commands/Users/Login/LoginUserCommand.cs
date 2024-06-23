using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Application.VOs;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Users.Application.Commands.Users.Login;

public class LoginUserCommand : ICommand<UserAuthVO>
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
