using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Application.Events;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Utils;
using System.Net;

namespace MotorcycleRental.Core.Presentation.Handlers;

public class ExceptionHandler(IApplicationEventSaver eventSaver) : IExceptionHandler
{
    private readonly IApplicationEventSaver _eventSaver = eventSaver;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var actionContext = httpContext.GetActionContext();

        if (!httpContext.Items.TryGetValue(ResponseConventionFilter.ERROR_IDENTIFIER, out var errorObj) || errorObj is not Error error)
        {
            error = InternalServerError.CreateUnkwnonError(exception);
        }

        var exceptionEvent = new ExceptionEvent(exception);

        await _eventSaver.SaveEventAsync(exceptionEvent);

        var errorResponse = new ErrorResponse()
        {
            Code = error.Code,
            Error = error.Description,
            EventId = exceptionEvent.Id.ToString(),
        };

        var result = new ObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
        };

        await result.ExecuteResultAsync(actionContext);

        return true;
    }
}
