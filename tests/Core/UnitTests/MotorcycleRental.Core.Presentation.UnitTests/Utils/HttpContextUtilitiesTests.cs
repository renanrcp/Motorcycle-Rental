using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Core.Presentation.Utils;

namespace MotorcycleRental.Core.Presentation.UnitTests.Utils
{
    public class HttpContextUtilitiesTests
    {
        [Fact]
        public void GetActionContext_ShouldReturnActionContextFromAccessor_WhenAccessorIsPresent()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                ActionDescriptor = new ControllerActionDescriptor(),
                RouteData = new RouteData(),
            };

            var actionContextAccessor = new ActionContextAccessor { ActionContext = actionContext };
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IActionContextAccessor>(actionContextAccessor)
                .BuildServiceProvider();
            httpContext.RequestServices = serviceProvider;

            // Act
            var result = httpContext.GetActionContext();

            // Assert
            Assert.Equal(actionContext, result);
        }

        [Fact]
        public void GetActionContext_ShouldCreateNewActionContext_WhenAccessorIsNotPresent()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var serviceProvider = new ServiceCollection().BuildServiceProvider();
            httpContext.RequestServices = serviceProvider;

            // Act
            var result = httpContext.GetActionContext();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(httpContext, result.HttpContext);
            Assert.NotNull(result.ActionDescriptor);
            Assert.NotNull(result.RouteData);
            Assert.NotNull(result.ModelState);
        }

        [Fact]
        public void CreateActionContext_ShouldCreateValidActionContext()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var routeData = new RouteData();
            var endpoint = new Endpoint(context => Task.CompletedTask, new EndpointMetadataCollection(new ControllerActionDescriptor()), "TestEndpoint");
            httpContext.SetEndpoint(endpoint);
            httpContext.Features.Set<IRoutingFeature>(new RoutingFeature
            {
                RouteData = routeData,
            });

            // Act
            var result = HttpContextUtilities.GetActionContext(httpContext);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(httpContext, result.HttpContext);
            Assert.Equal(routeData, result.RouteData);
            Assert.NotNull(result.ActionDescriptor);
            Assert.IsType<ControllerActionDescriptor>(result.ActionDescriptor);
        }
    }
}
