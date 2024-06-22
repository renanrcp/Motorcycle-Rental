using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record UnprocessableEntityError : Error
{
    public UnprocessableEntityError(string Code, string? Description = null) : base(Code, Description)
    {
    }
}
