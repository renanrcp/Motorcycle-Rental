using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record BadRequestError : Error
{
    public BadRequestError(string Code, string? Description = null, string? Property = null) : base(Code, Description)
    {
        this.Property = Property;
    }

    public string? Property { get; }
}
