using Microsoft.AspNetCore.Authorization;
using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Core.Presentation.Requirements;

public class RequirePermissionsAttribute(params PermissionType[] permissions) : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
{
    public PermissionType[] Permissions { get; } = permissions;

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}
