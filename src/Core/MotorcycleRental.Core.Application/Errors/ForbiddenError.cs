using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Errors;

public record ForbiddenError : Error
{
    public static readonly ForbiddenError NotPermission = new("MotorcycleRental.Forbidden.Permission", "Esse usuário não tem permissão para acessar esse recurso.");

    public ForbiddenError(string Code, string? Description = null) : base(Code, Description)
    {
    }
}
