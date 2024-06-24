using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Motorcycles.Application.Abstractions.Rentals;
using MotorcycleRental.Motorcycles.Infrastructure.Contexts;
using MotorcycleRental.Motorcycles.Infrastructure.Queries.Motorcycles;

namespace MotorcycleRental.Motorcycles.Application.Commands.Delete;

public class DeleteMotorycleCommandHandler
    (MotorcyclesDbContext dbContext, IRentalsService rentalsService) : ICommandHandler<DeleteMotorcycleCommand>
{
    private readonly MotorcyclesDbContext _dbContext = dbContext;
    private readonly IRentalsService _rentalsService = rentalsService;

    public async Task<Result> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var rentalByMotorcycleResult = await _rentalsService.GetRentalByMotorcycleAsync(new()
        {
            MotorcycleId = request.MotorycleId,
        });

        if (rentalByMotorcycleResult.IsSuccess)
        {
            return DeleteMotorycleErrors.RentalExists;
        }

        await _dbContext.Motorcycles
                        .WhereId(request.MotorycleId)
                        .ExecuteDeleteAsync(cancellationToken);

        return Result.Success;
    }
}
