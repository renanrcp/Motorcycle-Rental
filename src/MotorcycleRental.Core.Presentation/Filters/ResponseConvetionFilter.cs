using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Results;
using System.Net;

namespace MotorcycleRental.Core.Presentation.Filters;

public class ResponseConvetionFilter : IAsyncAlwaysRunResultFilter
{
    public const string ERROR_IDENTIFIER = "MotorcycleRental.HttpContext.Error";

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is not ErrorResult errorResult)
        {
            if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                context.Result = ConvertAspNetCoreBadRequestToErrorResponse(badRequestObjectResult);
                await next();
                return;
            }

            await next();
            return;
        }

        var error = errorResult.Error;

        var statusCode = error switch
        {
            BadRequestError => HttpStatusCode.BadRequest,
            ForbiddenError => HttpStatusCode.Forbidden,
            NotFoundError => HttpStatusCode.NotFound,
            UnprocessableEntityError => HttpStatusCode.UnprocessableEntity,
            InternalServerError => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError,
        };

        var exception = (error as InternalServerError)?.Exception;

        // Handle cases without exception throw.
        // WHY THIS CAN REALLY OCURR?
        if (exception != null)
        {
            context.HttpContext.Items.Add(ERROR_IDENTIFIER, error);
            var exceptionHandler = context.HttpContext.RequestServices.GetRequiredService<ExceptionHandler>();
            _ = await exceptionHandler.TryHandleAsync(context.HttpContext, exception, context.HttpContext.RequestAborted);
            await next();
            return;
        }

        var errorResponse = new ErrorResponse
        {
            Code = error.Code,
            Error = error.Description,
        };

        if (error is BadRequestError badRequestError)
        {
            var validationErrors = new Dictionary<string, IReadOnlyCollection<string>>();
            var validationError = new List<string>();

            if (!string.IsNullOrWhiteSpace(badRequestError.Description))
            {
                validationError.Add(badRequestError.Description);
            }

            validationErrors.Add(badRequestError.Property ?? "@", validationError);

            errorResponse.ValidationErrors = validationErrors;
        }

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = (int)statusCode,
        };
    }

    private static ObjectResult ConvertAspNetCoreBadRequestToErrorResponse(BadRequestObjectResult badRequestObjectResult)
    {
        if (badRequestObjectResult.Value is not ValidationProblemDetails validationProblemDetails)
        {
            throw new InvalidOperationException($"Cannot use {nameof(BadRequestObjectResult)} without a {nameof(ValidationProblemDetails)}.");
        }

        var validationErrors = new Dictionary<string, IReadOnlyCollection<string>>();

        foreach (var problemDetails in validationProblemDetails.Errors)
        {
            validationErrors.Add(problemDetails.Key, problemDetails.Value);
        }

        var errorResponse = new ErrorResponse
        {
            Code = AspNetCoreErrors.AspNetCoreValidationErrorCode,
            ValidationErrors = validationErrors,
        };

        return new ObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
        };
    }
}
