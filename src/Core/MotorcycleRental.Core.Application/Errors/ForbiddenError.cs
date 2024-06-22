using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record ForbiddenError : Error
{
    public ForbiddenError(string Code, string? Description = null) : base(Code, Description)
    {
    }
}
