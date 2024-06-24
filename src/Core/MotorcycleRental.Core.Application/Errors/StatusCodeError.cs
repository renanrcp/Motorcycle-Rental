using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record StatusCodeError : Error
{
    public StatusCodeError(int statusCode, string Code, string? Description = null) : base(Code, Description)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}
