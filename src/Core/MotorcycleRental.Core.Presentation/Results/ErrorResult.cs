using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Presentation.Results;

public class ErrorResult(Error error) : ObjectResult(error)
{
    public Error Error { get; } = error;
}
