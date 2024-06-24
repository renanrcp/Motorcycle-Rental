using System.Net.Http.Headers;

namespace MotorcycleRental.Core.Presentation.DelegatingHandlers;

public class AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorization = _httpContextAccessor.HttpContext?.Request.Headers.Authorization;

        if (authorization.HasValue)
        {
            var authorizationValue = authorization.Value.ToString().Replace("Bearer ", string.Empty);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationValue);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
