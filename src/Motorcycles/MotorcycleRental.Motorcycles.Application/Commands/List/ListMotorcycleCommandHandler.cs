using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Infrastructure.Queries.Motorcycles;

namespace MotorcycleRental.Motorcycles.Application.Commands.List;

public class ListMotorcycleCommandHandler(MotorcyclesDbContext dbContext) : ICommandHandler<ListMotorcycleCommand, IReadOnlyList<ListMotorcycleResponse>>
{
    private readonly MotorcyclesDbContext _dbContext = dbContext;

    public async Task<Result<IReadOnlyList<ListMotorcycleResponse>>> Handle(ListMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Motorcycles
                            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.LicensePlate))
        {
            query = query
                        .WhereLicensePlate(request.LicensePlate);
        }

        var motorcycles = await query
                                .Select(x => new ListMotorcycleResponse
                                {
                                    Id = x.Id,
                                    LicensePlate = x.LicensePlate,
                                    Model = x.Model,
                                    Year = x.Year,
                                })
                                .ToListAsync(cancellationToken);

        return motorcycles;
    }
}
