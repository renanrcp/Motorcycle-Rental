using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Users.Domain.Entities;

public class Role
{
    private readonly List<Permission> _permissions = [];

    private readonly List<User> _users = [];

    protected Role(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IReadOnlyList<Permission> Permissions => _permissions;

    public IReadOnlyList<User> Users => _users;

    public IReadOnlyList<PermissionType> GetAllPermissions()
    {
        return Permissions.Select(p => p.ToEnum()).ToList();
    }

    public static Result<Role> Create(string name)
    {
        return new Role(name);
    }
}
