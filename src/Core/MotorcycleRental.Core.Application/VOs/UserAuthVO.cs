using Microsoft.AspNetCore.Http;
using MotorcycleRental.Core.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace MotorcycleRental.Core.Application.VOs;

public class UserAuthVO
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IEnumerable<PermissionType> Permissions { get; set; }

    public string? Token { get; set; }

    public IEnumerable<Claim> ToClaims()
    {
        var permissionsStr = JsonSerializer.Serialize(Permissions.Select(x => Enum.GetName(x)));

        return
        [
            new Claim("id", Id.ToString(), ClaimValueTypes.Integer),
            new Claim("name", Name, ClaimValueTypes.String),
            new Claim("permissions", permissionsStr, JsonClaimValueTypes.JsonArray),
        ];
    }

    public static UserAuthVO FromHttpContext(HttpContext httpContext, bool parseToken = false)
    {
        if (httpContext.User.Identity?.IsAuthenticated != true)
        {
            throw new InvalidOperationException("Cannot parse a UserAuthVO from a non authenticated HttpContext.");
        }

        var claims = httpContext.User.Claims;

        var userId = claims
                        .Where(cl => cl.Type == "id")
                        .Select(cl => int.Parse(cl.Value))
                        .First();

        var name = claims
                        .Where(cl => cl.Type == "name")
                        .Select(cl => cl.Value)
                        .First();

        var permissionsClaim = claims
                            .Where(cl => cl.Type == "permissions")
                            .Select(cl => cl.Value)
                            .FirstOrDefault();

        var permissions =
            string.IsNullOrWhiteSpace(permissionsClaim)
                ? []
                : permissionsClaim?.StartsWith('[') == true
                    ? JsonSerializer.Deserialize<string[]>(permissionsClaim)!.Select(Enum.Parse<PermissionType>)
                    : [Enum.Parse<PermissionType>(permissionsClaim!)];

        var token = parseToken
            ? httpContext.Request.Headers.Authorization
            : default;

        return new()
        {
            Id = userId,
            Name = name,
            Permissions = permissions,
            Token = token,
        };
    }
}
