using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MotorcycleRental.Core.Presentation.Utils;

public static class HttpContextUtilities
{
    public static ActionContext GetActionContext(this HttpContext context)
    {
        var actionContextAccessor = context.RequestServices?.GetService<IActionContextAccessor>();

        return actionContextAccessor?.ActionContext ?? CreateActionContext(context);
    }

    private static ActionContext CreateActionContext(HttpContext httpContext)
    {
        var routeData = httpContext.GetRouteData() ?? new RouteData();
        var endpoint = httpContext.GetEndpoint();
        var actionDescriptor = endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>() ?? new ActionDescriptor();

        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

        return actionContext;
    }
}
