using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Users.Application.Errors.Users;
using MotorcycleRental.Users.Application.Queries.Users;
using MotorcycleRental.Users.Domain.Entities;
using MotorcycleRental.Users.Infrastructure.Contexts;

using BC = BCrypt.Net.BCrypt;


namespace MotorcycleRental.Users.Application.Commands.Users;

public class CreateUserCommandHandler(UsersDbContext usersDbContext) : ICommandHandler<CreateUserCommand, User>
{
    private readonly UsersDbContext _usersDbContext = usersDbContext;

    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

        return user;
    }
}
