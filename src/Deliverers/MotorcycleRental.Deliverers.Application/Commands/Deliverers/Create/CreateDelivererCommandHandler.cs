using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Deliverers.Application.Abstractions.Users;
using MotorcycleRental.Deliverers.Domain.Entities;
using MotorcycleRental.Deliverers.Infrastructure.Contexts;
using MotorcycleRental.Deliverers.Infrastructure.Queries;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Create;

public class CreateDelivererCommandHandler(DeliverersDbContext dbContext, IUsersService usersService)
        : ICommandHandler<CreateDelivererCommand, CreateDelivererResponse>
{
    private readonly DeliverersDbContext _dbContext = dbContext;

    private readonly IUsersService _usersService = usersService;

    public async Task<Result<CreateDelivererResponse>> Handle(CreateDelivererCommand request, CancellationToken cancellationToken)
    {
        var delivererCnhExists = await _dbContext.Deliverers
                                        .WhereCnh(request.Cnh)
                                        .AnyAsync(cancellationToken);

        if (delivererCnhExists)
        {
            return CreateDelivererErrors.CnhExists;
        }

        var delivererCnpjExists = await _dbContext.Deliverers
                                        .WhereCnpj(request.Cnpj)
                                        .AnyAsync(cancellationToken);

        if (delivererCnpjExists)
        {
            return CreateDelivererErrors.CnpjExists;
        }

        var userResult = await _usersService.CreateUserAsync(new()
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
        });

        if (userResult.IsFaulted)
        {
            return userResult.Error!;
        }

        var user = userResult.Value!;

        var createDelivererResult = Deliverer.Create(
            user.Id,
            user.Email,
            request.Cnh,
            request.CnhType,
            request.Name,
            request.BirthDate
        );

        if (createDelivererResult.IsFaulted)
        {
            return createDelivererResult.Error!;
        }

        var deliverer = createDelivererResult.Value!;

        await _dbContext.Deliverers.AddAsync(deliverer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateDelivererResponse
        {
            Id = deliverer.Id,
            Cnh = deliverer.Cnh,
            BirthDate = deliverer.BirthDate,
            CnhType = deliverer.CnhType,
            Cnpj = deliverer.Cnpj,
            Name = deliverer.Name,
            Email = user.Email,
        };
    }
}
