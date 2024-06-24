using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Users.Infrastructure.Queries.Users;
using MotorcycleRental.Users.Domain.Entities;
using MotorcycleRental.Users.Infrastructure.Contexts;

using BC = BCrypt.Net.BCrypt;
using System.Data.Common;


namespace MotorcycleRental.Users.Application.Commands.Users.Create;

public class CreateUserCommandHandler(UsersDbContext usersDbContext) : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly UsersDbContext _usersDbContext = usersDbContext;

    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExistsWithEmail = await _usersDbContext.Users
                                            .WhereEmail(request.Email)
                                            .AnyAsync(cancellationToken);

        if (userExistsWithEmail)
        {
            return CreateUserErrors.EmailExists;
        }

        var hashPassword = BC.HashPassword(request.Password);

        var createUserResult = User.Create(
                request.Email,
                request.Name,
                hashPassword);

        if (createUserResult.IsFaulted)
        {
            return createUserResult.Error!;
        }

        var user = createUserResult.Value!;

        await _usersDbContext.Users.AddAsync(user, cancellationToken);
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Permissions = user.GetAllPermissions(),
        };
    }
}
