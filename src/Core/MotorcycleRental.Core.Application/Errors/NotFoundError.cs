using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record NotFoundError : Error
{
    public NotFoundError(string Code, string? Description = null) : base(Code, Description)
    {
    }
}
