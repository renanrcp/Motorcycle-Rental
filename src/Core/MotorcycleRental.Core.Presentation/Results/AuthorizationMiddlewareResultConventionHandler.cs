using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Utils;
using System.Net;

namespace MotorcycleRental.Core.Presentation.Results;

public class AuthorizationMiddlewareResultConventionHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded)
        {
            await next(context);
            return;
        }

        string? errorMessage = null;

        if (authorizeResult.Forbidden)
        {
            var forbiddenErrorMessage = authorizeResult.AuthorizationFailure?.FailureReasons?.FirstOrDefault()?.Message;

            if (!string.IsNullOrWhiteSpace(forbiddenErrorMessage))
            {
                errorMessage = forbiddenErrorMessage;
            }
        }

        Error error = authorizeResult.Forbidden
                ? ForbiddenError.NotPermission
                : UnauthorizedError.DefaultUnauthorized;

        var errorResponse = new ErrorResponse
        {
            Code = error.Code,
            Error = errorMessage ?? error.Description,
        };

        var result = new ObjectResult(errorResponse)
        {
            StatusCode = authorizeResult.Forbidden
                ? (int)HttpStatusCode.Forbidden
                : (int)HttpStatusCode.Unauthorized,
        };

        var actionContext = context.GetActionContext();

        await result.ExecuteResultAsync(actionContext);
    }
}
