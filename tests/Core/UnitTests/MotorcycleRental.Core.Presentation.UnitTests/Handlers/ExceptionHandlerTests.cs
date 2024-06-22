using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Application.Events;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Handlers;
using MotorcycleRental.Core.Presentation.Responses;

namespace MotorcycleRental.Core.Presentation.UnitTests.Handlers;

public class ExceptionHandlerTests
{
    private readonly Mock<IApplicationEventSaver> _mockEventSaver;
    private readonly Mock<IActionResultExecutor<ObjectResult>> _mockExecutor;
    private readonly ExceptionHandler _exceptionHandler;

    public ExceptionHandlerTests()
    {
        _mockEventSaver = new Mock<IApplicationEventSaver>();
        _mockExecutor = new Mock<IActionResultExecutor<ObjectResult>>();
        _exceptionHandler = new ExceptionHandler(_mockEventSaver.Object);
    }

    [Fact]
    public async Task TryHandleAsync_ShouldHandleExceptionAndSaveEvent()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var exception = new Exception("Test exception");
        var cancellationToken = CancellationToken.None;

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
        httpContext.Items[ResponseConventionFilter.ERROR_IDENTIFIER] = InternalServerError.CreateUnkwnonError(exception);

        _mockEventSaver
            .Setup(saver => saver.SaveEventAsync(It.IsAny<ExceptionEvent>()))
            .Returns(Task.CompletedTask);

        _mockExecutor
            .Setup(executor => executor.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _exceptionHandler.TryHandleAsync(httpContext, exception, cancellationToken);

        // Assert
        Assert.True(result);

        _mockEventSaver.Verify(saver => saver.SaveEventAsync(It.Is<ExceptionEvent>(e => e.Exception == exception)), Times.Once);
        _mockExecutor.Verify(executor => executor.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()), Times.Once);


        var executedResult = (ObjectResult)_mockExecutor.Invocations[0].Arguments[1];
        var errorResponse = Assert.IsType<ErrorResponse>(executedResult.Value);
        Assert.Equal((int)HttpStatusCode.InternalServerError, executedResult.StatusCode);
        Assert.Equal(InternalServerError.CreateUnkwnonError(exception).Code, errorResponse.Code);
        Assert.Equal(InternalServerError.CreateUnkwnonError(exception).Description, errorResponse.Error);
        Assert.NotNull(errorResponse.EventId);
    }

    [Fact]
    public async Task TryHandleAsync_ShouldHandleExceptionWithoutErrorInHttpContextItems()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var exception = new Exception("Test exception");
        var cancellationToken = CancellationToken.None;

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

        _mockEventSaver
            .Setup(saver => saver.SaveEventAsync(It.IsAny<ExceptionEvent>()))
            .Returns(Task.CompletedTask);

        _mockExecutor
            .Setup(executor => executor.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _exceptionHandler.TryHandleAsync(httpContext, exception, cancellationToken);

        // Assert
        Assert.True(result);

        _mockEventSaver.Verify(saver => saver.SaveEventAsync(It.Is<ExceptionEvent>(e => e.Exception == exception)), Times.Once);
        _mockExecutor.Verify(executor => executor.ExecuteAsync(It.IsAny<ActionContext>(), It.IsAny<ObjectResult>()), Times.Once);

        var executedResult = (ObjectResult)_mockExecutor.Invocations[0].Arguments[1];
        var errorResponse = Assert.IsType<ErrorResponse>(executedResult.Value);
        Assert.Equal((int)HttpStatusCode.InternalServerError, executedResult.StatusCode);
        Assert.Equal(InternalServerError.CreateUnkwnonError(exception).Code, errorResponse.Code);
        Assert.Equal(InternalServerError.CreateUnkwnonError(exception).Description, errorResponse.Error);
        Assert.NotNull(errorResponse.EventId);
    }
}
