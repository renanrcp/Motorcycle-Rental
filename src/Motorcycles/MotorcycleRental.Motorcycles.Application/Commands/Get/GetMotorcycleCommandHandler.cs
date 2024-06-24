using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Infrastructure.Queries.Motorcycles;

namespace MotorcycleRental.Motorcycles.Application.Commands.Get;

public class GetMotorcycleCommandHandler(MotorcyclesDbContext dbContext) : ICommandHandler<GetMotorcycleCommand, GetMotorcycleResponse>
{
    private readonly MotorcyclesDbContext _dbContext = dbContext;

    public async Task<Result<GetMotorcycleResponse>> Handle(GetMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _dbContext.Motorcycles
                                    .AsNoTracking()
                                    .WhereId(request.MotorcycleId)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (motorcycle == null)
        {
            return GetMotorcycleErrors.MotorcycleNotFound;
        }

        return new GetMotorcycleResponse
        {
            Id = motorcycle.Id,
            LicensePlate = motorcycle.LicensePlate,
            Model = motorcycle.Model,
            Year = motorcycle.Year,
        };
    }
}
