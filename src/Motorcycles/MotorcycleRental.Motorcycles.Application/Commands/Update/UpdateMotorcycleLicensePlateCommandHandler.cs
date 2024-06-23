using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Infrastructure.Queries.Motorcycles;

namespace MotorcycleRental.Motorcycles.Application.Commands.Update;

public class UpdateMotorcycleLicensePlateCommandHandler(MotorcyclesDbContext dbContext)
        : ICommandHandler<UpdateMotorcycleLicensePlateCommand, UpdateMotorcycleLicensePlateResponse>
{
    private readonly MotorcyclesDbContext _dbContext = dbContext;

    public async Task<Result<UpdateMotorcycleLicensePlateResponse>> Handle(UpdateMotorcycleLicensePlateCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _dbContext.Motorcycles
                                    .AsTracking()
                                    .WhereId(request.Id)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (motorcycle == null)
        {
            return UpdateMotorcycleErrors.MotorcycleNotFound;
        }

        var motorcycleLicensePlateExists = await _dbContext.Motorcycles
                                            .WhereLicensePlate(request.LicensePlate)
                                            .Where(x => x.Id != request.Id)
                                            .AnyAsync(cancellationToken);

        if (motorcycleLicensePlateExists)
        {
            return UpdateMotorcycleErrors.MotorcycleExists;
        }

        motorcycle.SetLicensePlate(request.LicensePlate);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateMotorcycleLicensePlateResponse
        {
            Id = motorcycle.Id,
            LicensePlate = motorcycle.LicensePlate,
            Model = motorcycle.Model,
            Year = motorcycle.Year,
        };
    }
}
