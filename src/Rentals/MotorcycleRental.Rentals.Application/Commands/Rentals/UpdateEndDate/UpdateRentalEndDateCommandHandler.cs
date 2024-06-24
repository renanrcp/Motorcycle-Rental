using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Rentals.Application.Abstractions.Deliverers;
using MotorcycleRental.Rentals.Infrastructure.Contexts;
using MotorcycleRental.Rentals.Infrastructure.Queries;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.UpdateEndDate;

public class UpdateRentalEndDateCommandHandler(RentalsDbContext dbContext, IDeliverersService deliverersService) : ICommandHandler<UpdateRentalEndDateCommand, UpdateRentalEndDateResponse>
{
    private readonly IDeliverersService _deliverersService = deliverersService;

    private readonly RentalsDbContext _dbContext = dbContext;

    public async Task<Result<UpdateRentalEndDateResponse>> Handle(UpdateRentalEndDateCommand request, CancellationToken cancellationToken)
    {
        var currentDelivererResult = await _deliverersService.GetCurrentDelivererAsync();

        if (currentDelivererResult.IsFaulted)
        {
            return currentDelivererResult.Error!;
        }

        var rental = await _dbContext.Rentals
                                    .AsTracking()
                                    .Include(x => x.RentalType)
                                    .WhereDelivererId(currentDelivererResult.Value!.Id)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (rental == null)
        {
            return UpdateRentalEndDateErrors.RentalNotFound;
        }

        var setEndDateResult = rental.SetEndDate(request.EndDate);

        if (setEndDateResult.IsFaulted)
        {
            return setEndDateResult.Error!;
        }

        var costResult = rental.CalculateCost();

        if (costResult.IsFaulted)
        {
            return costResult.Error!;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateRentalEndDateResponse
        {
            DelivererId = rental.DelivererId,
            EndDate = rental.EndDate!.Value,
            ExpectedEndDate = rental.ExpectedEndDate,
            MotorcycleId = rental.MotorcycleId,
            RentalTypeId = rental.RentalTypeId,
            StartDate = rental.StartDate,
            TotalCost = costResult.Value!,
        };
    }
}
