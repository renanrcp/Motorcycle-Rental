using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Utils;
using System.Net;

namespace MotorcycleRental.Core.Presentation.Handlers;

public class ExceptionHandler : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var actionContext = httpContext.GetActionContext();

        if (!httpContext.Items.TryGetValue(ResponseConvetionFilter.ERROR_IDENTIFIER, out var errorObj) || errorObj is not Error error)
        {
            error = InternalServerError.CreateUnkwnonError(exception);
        }

        // TODO: Register error event
        var eventId = Guid.NewGuid().ToString();

        var errorResponse = new ErrorResponse()
        {
            Code = error.Code,
            Error = error.Description,
            EventId = eventId,
        };

        var result = new OkObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
        };

        await result.ExecuteResultAsync(actionContext);

        return true;
    }
}
