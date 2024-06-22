using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record InternalServerError : Error
{
    public InternalServerError(string Code, string? Description = null, Exception? Exception = null) : base(Code, Description)
    {
        this.Exception = Exception;
    }

    public Exception? Exception { get; }

    public static InternalServerError CreateUnkwnonError(Exception exception)
    {
        return new InternalServerError("Error.Unknown", "Erro Interno", exception);
    }
}
