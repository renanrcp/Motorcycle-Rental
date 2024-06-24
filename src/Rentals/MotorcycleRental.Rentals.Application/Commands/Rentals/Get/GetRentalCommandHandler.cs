using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Rentals.Application.Abstractions.Deliverers;
using MotorcycleRental.Rentals.Infrastructure.Contexts;
using MotorcycleRental.Rentals.Infrastructure.Queries;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.Get;

public class GetRentalCommandHandler(
    RentalsDbContext dbContext,
    IDeliverersService deliverersService) : ICommandHandler<GetRentalCommand, GetRentalResponse>
{
    private readonly RentalsDbContext _dbContext = dbContext;
    private readonly IDeliverersService _deliverersService = deliverersService;

    public async Task<Result<GetRentalResponse>> Handle(GetRentalCommand request, CancellationToken cancellationToken)
    {
        var currentDelivererResult = await _deliverersService.GetCurrentDelivererAsync();

        if (currentDelivererResult.IsFaulted)
        {
            return currentDelivererResult.Error!;
        }

        var rental = await _dbContext.Rentals
                                    .AsNoTracking()
                                    .Include(x => x.RentalType)
                                    .WhereDelivererId(currentDelivererResult.Value!.Id)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (rental == null)
        {
            return GetRentalErrors.RentalNotFound;
        }

        var costResult = rental.CalculateCost();

        if (costResult.IsFaulted)
        {
            return costResult.Error!;
        }

        return new GetRentalResponse
        {
            DelivererId = rental.DelivererId,
            EndDate = rental.EndDate,
            ExpectedEndDate = rental.ExpectedEndDate,
            MotorcycleId = rental.MotorcycleId,
            RentalTypeId = rental.RentalTypeId,
            StartDate = rental.StartDate,
            TotalCost = costResult.Value!,
        };
    }
}
