using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Users.Domain.Events;

namespace MotorcycleRental.Users.Domain.Entities;

public class User : Entity
{
    private readonly List<Role> _roles = [];

    protected User(string email, string name, string password)
    {
        Email = email;
        Name = name;
        Password = password;

        Raise(new UserCreatedEvent
        {
            Email = email,
        });
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

    public static Result<User> Create(string email, string name, string password)
    {
        return new User(email, name, password);
    }
}
