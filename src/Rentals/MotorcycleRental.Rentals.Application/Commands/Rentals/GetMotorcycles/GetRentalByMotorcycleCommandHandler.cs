using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Rentals.Infrastructure.Contexts;
using MotorcycleRental.Rentals.Infrastructure.Queries;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.GetMotorcycles;

public class GetRentalByMotorcycleCommandHandler(RentalsDbContext dbContext) : ICommandHandler<GetRentalByMotorcycleCommand, GetRentalByMotorcycleResponse>
{
    private readonly RentalsDbContext _dbContext = dbContext;

    public async Task<Result<GetRentalByMotorcycleResponse>> Handle(GetRentalByMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var rental = await _dbContext.Rentals
                                .AsNoTracking()
                                .WhereMotorycleId(request.MotorcycleId)
                                .FirstOrDefaultAsync(cancellationToken);

        if (rental == null)
        {
            return GetRentalByMotorcycleErrors.RentalNotFound;
        }

        return new GetRentalByMotorcycleResponse
        {
            DelivererId = rental.DelivererId,
            EndDate = rental.EndDate,
            ExpectedEndDate = rental.ExpectedEndDate,
            MotorcycleId = rental.MotorcycleId,
            RentalTypeId = rental.RentalTypeId,
            StartDate = rental.StartDate,
        };
    }
}
