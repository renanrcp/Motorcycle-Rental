using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Users.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Users.Application.Commands.Users.Create;

public class CreateUserCommand : ICommand<CreateUserResponse>
{
    [Required]
    public required string Name { get; init; }

    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
