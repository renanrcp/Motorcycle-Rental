using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Deliverers.Application.Validators;
using MotorcycleRental.Deliverers.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;

public class CreateDelivererCommand : ICommand<CreateDelivererResponse>
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    [Cnpj]
    public required string Cnpj { get; init; }

    [Required]
    [Cnh]
    public required string Cnh { get; init; }

    [Required]
    public required CnhType CnhType { get; init; }

    [Required]
    public required DateOnly BirthDate { get; init; }
}
