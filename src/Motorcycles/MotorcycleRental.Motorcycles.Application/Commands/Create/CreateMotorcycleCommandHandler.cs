using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Motorcycles.Domain.Entities;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Infrastructure.Queries.Motorcycles;

namespace MotorcycleRental.Motorcycles.Application.Commands.Create;

public class CreateMotorcycleCommandHandler(MotorcyclesDbContext dbContext)
    : ICommandHandler<CreateMotorycleCommand, CreateMotorcycleResponse>
{
    private readonly MotorcyclesDbContext _dbContext = dbContext;

    public async Task<Result<CreateMotorcycleResponse>> Handle(CreateMotorycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycleExists = await _dbContext.Motorcycles
                                        .WhereLicensePlate(request.LicensePlate)
                                        .AnyAsync(cancellationToken);

        if (motorcycleExists)
        {
            return CreateMotorcycleErrors.MotorcycleExists;
        }

        var motorcycleResult = Motorcycle.Create(request.LicensePlate, request.Year, request.Model);

        if (motorcycleResult.IsFaulted)
        {
            return motorcycleResult.Error!;
        }

        var motorcycle = motorcycleResult.Value!;

        await _dbContext.Motorcycles.AddAsync(motorcycle, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateMotorcycleResponse
        {
            Id = motorcycle.Id,
            LicensePlate = motorcycle.LicensePlate,
            Year = motorcycle.Year,
            Model = motorcycle.Model,
        };
    }
}
