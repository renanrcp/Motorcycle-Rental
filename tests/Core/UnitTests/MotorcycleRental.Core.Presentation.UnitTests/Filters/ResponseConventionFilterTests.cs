using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using MotorcycleRental.Core.Application.Errors;
using MotorcycleRental.Core.Presentation.Filters;
using MotorcycleRental.Core.Presentation.Responses;
using MotorcycleRental.Core.Presentation.Results;

namespace MotorcycleRental.Core.Presentation.UnitTests.Filters;

public class ResponseConventionFilterTests
{
    private readonly ResponseConventionFilter _filter;
    private readonly HttpContext _httpContextMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly Mock<IExceptionHandler> _exceptionHandlerMock;
    private readonly ResultExecutingContext _context;
    private readonly ResultExecutionDelegate _next;

    public ResponseConventionFilterTests()
    {
        _filter = new ResponseConventionFilter();
        _httpContextMock = new DefaultHttpContext();
        _serviceProviderMock = new Mock<IServiceProvider>();
        _exceptionHandlerMock = new Mock<IExceptionHandler>();


        _httpContextMock.RequestServices = _serviceProviderMock.Object;

        var actionContext = new ActionContext
        {
            HttpContext = _httpContextMock,
            ActionDescriptor = new ActionDescriptor(),
            RouteData = new RouteData(),
        };

        _context = new ResultExecutingContext(actionContext, [], new ObjectResult(null), new object());
        _next = () => Task.FromResult(new ResultExecutedContext(actionContext, [], new ObjectResult(null), new object()));
    }

    [Fact]
    public async Task OnResultExecutionAsync_ShouldHandleBadRequestObjectResult()
    {
        // Arrange
        var validationProblemDetails = new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "field1", new[] { "error1" } },
        });

        _context.Result = new BadRequestObjectResult(validationProblemDetails);

        // Act
        await _filter.OnResultExecutionAsync(_context, _next);

        // Assert
        var result = Assert.IsType<ObjectResult>(_context.Result);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponse>(result.Value);
        Assert.Equal(AspNetCoreErrors.AspNetCoreValidationErrorCode, errorResponse.Code);
        Assert.NotNull(errorResponse.ValidationErrors);
        Assert.Single(errorResponse.ValidationErrors);
    }

    [Fact]
    public async Task OnResultExecutionAsync_ShouldHandleErrorResult_WithBadRequestError()
    {
        // Arrange
        var badRequestError = new BadRequestError("code", "description", "property");
        _context.Result = new ErrorResult(badRequestError);

        // Act
        await _filter.OnResultExecutionAsync(_context, _next);

        // Assert
        var result = Assert.IsType<ObjectResult>(_context.Result);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponse>(result.Value);
        Assert.Equal(badRequestError.Code, errorResponse.Code);
        Assert.NotNull(errorResponse.ValidationErrors);
        Assert.Single(errorResponse.ValidationErrors);
    }

    [Fact]
    public async Task OnResultExecutionAsync_ShouldHandleErrorResult_WithInternalServerError()
    {
        // Arrange
        var internalServerError = InternalServerError.CreateUnkwnonError(new Exception("test"));
        _context.Result = new ErrorResult(internalServerError);
        _serviceProviderMock.Setup(x => x.GetService(typeof(IExceptionHandler)))
            .Returns(_exceptionHandlerMock.Object);

        // Act
        await _filter.OnResultExecutionAsync(_context, _next);

        // Assert
        _exceptionHandlerMock.Verify(
            x => x.TryHandleAsync(_httpContextMock, internalServerError.Exception!, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task OnResultExecutionAsync_ShouldHandleErrorResult_WithForbiddenError()
    {
        // Arrange
        var forbiddenError = new ForbiddenError("code", "description");
        _context.Result = new ErrorResult(forbiddenError);

        // Act
        await _filter.OnResultExecutionAsync(_context, _next);

        // Assert
        var result = Assert.IsType<ObjectResult>(_context.Result);
        Assert.Equal((int)HttpStatusCode.Forbidden, result.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponse>(result.Value);
        Assert.Equal(forbiddenError.Code, errorResponse.Code);
    }
}
