using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Users.Domain.Entities;

public class Permission
{
    private readonly List<Role> _roles = [];

    protected internal Permission(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IReadOnlyList<Role> Roles => _roles;

    public PermissionType ToEnum()
    {
        return Enum.Parse<PermissionType>(Name);
    }

    public static Result<Permission> Create(string name)
    {
        return new Permission(name);
    }
}
