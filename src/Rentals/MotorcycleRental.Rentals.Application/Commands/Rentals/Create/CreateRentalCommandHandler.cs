using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Deliverers.Domain.Entities;
using MotorcycleRental.Rentals.Application.Abstractions.Deliverers;
using MotorcycleRental.Rentals.Application.Abstractions.Motorcycles;
using MotorcycleRental.Rentals.Domain.Entities;
using MotorcycleRental.Rentals.Infrastructure.Contexts;
using MotorcycleRental.Rentals.Infrastructure.Queries;

namespace MotorcycleRental.Rentals.Application.Commands.Rentals.Create;

public class CreateRentalCommandHandler(
    RentalsDbContext dbContext,
    IDeliverersService deliverersService,
    IMotorcyclesService motorcyclesService) : ICommandHandler<CreateRentalCommand, CreateRentalResponse>
{
    private readonly RentalsDbContext _dbContext = dbContext;

    private readonly IDeliverersService _deliverersService = deliverersService;

    private readonly IMotorcyclesService _motorcyclesService = motorcyclesService;

    public async Task<Result<CreateRentalResponse>> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var currentDelivererResult = await _deliverersService.GetCurrentDelivererAsync();

        if (currentDelivererResult.IsFaulted)
        {
            return currentDelivererResult.Error!;
        }

        var currentDeliverer = currentDelivererResult.Value!;

        if (currentDeliverer.CnhType == CnhType.B)
        {
            return CreateRentalErrors.DelivererInvalidCnhType;
        }

        var existsRentalDeliverer = await _dbContext.Rentals
                                            .WhereDelivererId(currentDeliverer.Id)
                                            .AnyAsync(cancellationToken);

        if (existsRentalDeliverer)
        {
            return CreateRentalErrors.RentalDelivererExists;
        }

        var existsRentalMotorcycle = await _dbContext.Rentals
                                            .WhereMotorycleId(request.MotorcycleId)
                                            .AnyAsync(cancellationToken);

        if (existsRentalMotorcycle)
        {
            return CreateRentalErrors.RentalMotorcycleExists;
        }

        var motorycleResult = await _motorcyclesService.GetMotorcycleAsync(new()
        {
            MotorcycleId = request.MotorcycleId,
        });

        if (motorycleResult.IsFaulted)
        {
            return motorycleResult.Error!;
        }

        var rentalType = await _dbContext.RentalTypes
                                    .AsTracking()
                                    .WhereId(request.RentalTypeId)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (rentalType == null)
        {
            return CreateRentalErrors.RentalTypeNotFound;
        }

        var createRentalResult = Rental.Create(rentalType, request.MotorcycleId, currentDeliverer.Id);

        if (createRentalResult.IsFaulted)
        {
            return createRentalResult.Error!;
        }

        var rental = createRentalResult.Value!;

        await _dbContext.Rentals.AddAsync(rental, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateRentalResponse
        {
            DelivererId = rental.DelivererId,
            ExpectedEndDate = rental.ExpectedEndDate,
            MotorcycleId = rental.MotorcycleId,
            RentalTypeId = rental.RentalTypeId,
            StartDate = rental.StartDate,
        };
    }
}
