using Microsoft.AspNetCore.Authorization;
using MotorcycleRental.Core.Application.VOs;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Core.Presentation.Requirements;

namespace MotorcycleRental.Core.Presentation.Handlers;

public class RequirePermissionsAuthorizationHandler(IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<RequirePermissionsAttribute>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequirePermissionsAttribute requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            context.Fail(new AuthorizationFailureReason(this, $"Cannot get {nameof(HttpContext)} inside {nameof(RequirePermissionsAuthorizationHandler)}."));
            return Task.CompletedTask;
        }

        if (httpContext.User.Identity?.IsAuthenticated != true)
        {
            context.Fail(new AuthorizationFailureReason(this, "User is not authenticated."));
            return Task.CompletedTask;
        }

        var userAuthVO = UserAuthVO.FromHttpContext(httpContext);

        if (!HasPermissions(userAuthVO.Permissions, requirement.Permissions))
        {
            context.Fail(new AuthorizationFailureReason(this, $"User '{userAuthVO.Id}' doesn't has required permissions."));
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }

    private static bool HasPermissions(IEnumerable<PermissionType> userPermissions, params PermissionType[] permissions)
    {
        if (userPermissions.Contains(PermissionType.All))
        {
            return true;
        }

        return permissions.All(p => userPermissions.Contains(p));
    }
}
