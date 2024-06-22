using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Results;

namespace MotorcycleRental.Core.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected internal IActionResult ToActionResult(Result result)
    {
        return result.Match(
            NoContent,
            ErrorToActionResult
        );
    }

    protected internal IActionResult ToActionResult<TValue>(Result<TValue> result)
    {
        return result.Match(
            (value) => Ok(value),
            ErrorToActionResult
        );
    }

    protected internal IActionResult ErrorToActionResult(Error error)
    {
        return new ErrorResult(error);
    }
}
