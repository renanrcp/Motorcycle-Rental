using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Users.Application.Queries.Users;
using MotorcycleRental.Users.Infrastructure.Contexts;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MotorcycleRental.Core.Application;
using MotorcycleRental.Core.Application.VOs;
using MediatR;


namespace MotorcycleRental.Users.Application.Commands.Users.Login;

public class LoginUserCommandHandler(UsersDbContext usersDbContext, IApplicationEventSaver applicationEventSaver) : ICommandHandler<LoginUserCommand, UserAuthVO>
{
    private readonly UsersDbContext _usersDbContext = usersDbContext;

    private readonly IApplicationEventSaver _applicationEventSaver = applicationEventSaver;

    public async Task<Result<UserAuthVO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersDbContext.Users
                                .AsNoTracking()
                                .Include(x => x.Roles)
                                    .ThenInclude(x => x.Permissions)
                                .WhereEmail(request.Email)
                                .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return LoginUserErrors.InvalidData;
        }

        if (!BC.Verify(request.Password, user.Password))
        {
            return LoginUserErrors.InvalidData;
        }

        var permissions = user.GetAllPermissions();

        var userAuth = new UserAuthVO
        {
            Id = user.Id,
            Name = user.Name,
            Permissions = permissions,
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userAuth.ToClaims()),
            Expires = DateTime.UtcNow.AddHours(6),
            Issuer = ApplicationCoreConstants.Issuer,
            Audience = ApplicationCoreConstants.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(ApplicationCoreConstants.Key)
                ),
                SecurityAlgorithms.HmacSha256
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        userAuth.Token = stringToken;

        await _applicationEventSaver.SaveEventAsync(new UserLoggedEvent
        {
            UserId = userAuth.Id,
        }, cancellationToken);

        return userAuth;
    }
}
