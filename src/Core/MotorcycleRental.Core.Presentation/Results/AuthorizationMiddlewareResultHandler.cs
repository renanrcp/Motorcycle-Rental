using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Utils;

namespace MotorcycleRental.Core.Presentation.Results;

public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded)
        {
            await next(context);
            return;
        }

        var error = UnauthorizedError.DefaultUnauthorized;

        var errorResponse = new ErrorResponse
        {
            Code = error.Code,
            Error = error.Description,
        };

        var result = new ObjectResult(errorResponse)
        {
            StatusCode = 401
        };

        var actionContext = context.GetActionContext();

        await result.ExecuteResultAsync(actionContext);
    }
}
