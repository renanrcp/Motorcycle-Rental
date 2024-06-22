

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Results;
using System.Net;

namespace MotorcycleRental.Core.Presentation.UnitTests.Results;

public class AuthorizationMiddlewareResultConventionHandlerTests
{
    private readonly Mock<IActionResultExecutor<ObjectResult>> _mockExecutor;
    private readonly AuthorizationMiddlewareResultConventionHandler _authorizationMiddlewareResultHandler;

    public AuthorizationMiddlewareResultConventionHandlerTests()
    {
        _mockExecutor = new Mock<IActionResultExecutor<ObjectResult>>();
        _authorizationMiddlewareResultHandler = new AuthorizationMiddlewareResultConventionHandler();
    }

    [Fact]
    public async Task HandleAsync_ShouldCallNext_WhenAuthorizationSucceeds()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var nextDelegate = new Mock<RequestDelegate>();
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        var authorizeResult = PolicyAuthorizationResult.Success();

        // Act
        await _authorizationMiddlewareResultHandler.HandleAsync(nextDelegate.Object, httpContext, policy, authorizeResult);

        // Assert
        nextDelegate.Verify(next => next(httpContext), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnUnauthorizedResponse_WhenAuthorizationFails()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var nextDelegate = new Mock<RequestDelegate>();
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        var authorizeResult = PolicyAuthorizationResult.Forbid();

        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            ActionDescriptor = new ControllerActionDescriptor(),
            RouteData = new RouteData(),
        };

        var services = new ServiceCollection();
        services.AddSingleton(_mockExecutor.Object);
        services.AddSingleton<IActionContextAccessor>(new ActionContextAccessor { ActionContext = actionContext });
        var serviceProvider = services.BuildServiceProvider();

        httpContext.RequestServices = serviceProvider;

        _mockExecutor
            .Setup(executor => executor.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()))
            .Returns(Task.CompletedTask);

        // Act
        await _authorizationMiddlewareResultHandler.HandleAsync(nextDelegate.Object, httpContext, policy, authorizeResult);

        // Assert
        nextDelegate.Verify(next => next(httpContext), Times.Never);

        _mockExecutor.Verify(executor => executor.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()), Times.Once);

        var executedResult = (ObjectResult)_mockExecutor.Invocations[0].Arguments[1];
        var errorResponse = Assert.IsType<ErrorResponse>(executedResult.Value);
        Assert.Equal((int)HttpStatusCode.Unauthorized, executedResult.StatusCode);
        Assert.Equal(UnauthorizedError.DefaultUnauthorized.Code, errorResponse.Code);
        Assert.Equal(UnauthorizedError.DefaultUnauthorized.Description, errorResponse.Error);
    }
}
