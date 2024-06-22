using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;

namespace MotorcycleRental.Users.Domain.Entities;

public class User : Entity
{
    private readonly List<Role> _roles = [];

    protected User(string email, string name, string password)
    {
        Email = email;
        Name = name;
        Password = password;
    }

    public string Email { get; private set; }

    public string Name { get; private set; }

    public string Password { get; private set; }

    public IReadOnlyList<Role> Roles => _roles;

    public IReadOnlyList<PermissionType> GetAllPermissions()
    {
        var allRolesPermissions = Roles
                                    .SelectMany(r => r.GetAllPermissions());

        return allRolesPermissions.ToList();
    }

    private Result AddRole(Role role)
    {
        _roles.Add(role);

        return Result.Success;
    }

    public static Result<User> Create(string email, string name, string password)
    {
        return new User(email, name, password);
    }

    public static Result<User> CreateUserWithRole(string email, string name, string password, Role role)
    {
        var userResult = Create(email, name, password);

        if (userResult.IsFaulted)
        {
            return userResult;
        }

        var user = userResult.Value!;

        var roleResult = user.AddRole(role);

        return roleResult.Map(() => user);
    }
}
