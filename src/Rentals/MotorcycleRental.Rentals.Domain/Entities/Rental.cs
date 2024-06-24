using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Rentals.Domain.Errors;

namespace MotorcycleRental.Rentals.Domain.Entities;

public class Rental : Entity
{
    private Rental(int motorcycleId, int delivererId, int rentalTypeId, DateTime startDate, DateTime expectedEndDate)
    {
        MotorcycleId = motorcycleId;
        DelivererId = delivererId;
        RentalTypeId = rentalTypeId;
        StartDate = startDate;
        ExpectedEndDate = expectedEndDate;
    }

    public int MotorcycleId { get; private set; }

    public int DelivererId { get; private set; }

    public int RentalTypeId { get; private set; }

    public RentalType? RentalType { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime ExpectedEndDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    internal void SetRentalType(RentalType type)
    {
        RentalType = type;
    }

    public Result SetEndDate(DateTime endDate)
    {
        var finalEndDate = endDate.Date;

        if (finalEndDate < StartDate)
        {
            return RentalErrors.EndDateLowerThanStartDate;
        }

        EndDate = finalEndDate;

        return Result.Success;
    }

    public Result<decimal> CalculateCost()
    {
        if (RentalType == null)
        {
            return RentalErrors.RentalTypeUnavailable;
        }

        if (!EndDate.HasValue || EndDate.Value == ExpectedEndDate)
        {
            return RentalType.Days * RentalType.Cost;
        }

        int daysDiff;
        decimal totalDaysCost, daysDiffCost;

        if (EndDate.Value < ExpectedEndDate)
        {
            // START: 01
            // Expected 16
            // End 14
            // 14 - 1 = 13
            // 16 - 14 = 2
            // (13×28) + ((2×28)1,40)

            var totalDays = (EndDate.Value - StartDate).Days;
            daysDiff = (ExpectedEndDate - EndDate.Value).Days;

            var penaltyPercentage = RentalType.Days >= 15
                ? 1.40m
                : 1.20m;

            totalDaysCost = totalDays * RentalType.Cost;
            daysDiffCost = daysDiff * RentalType.Cost * penaltyPercentage;

            return totalDaysCost + daysDiffCost;
        }

        // Start 01
        // Expected 16
        // End 20
        // 20 - 16 = 4
        // (15 * 28) + (4 * 50)

        daysDiff = (EndDate.Value - ExpectedEndDate).Days;

        daysDiffCost = daysDiff * 50;
        totalDaysCost = RentalType.Days * RentalType.Cost;

        return totalDaysCost + daysDiffCost;
    }

    public static Result<Rental> Create(
        RentalType rentalType,
        int motorcycleId,
        int delivererId)
    {
        if (rentalType == null)
        {
            return RentalErrors.RentalTypeNotExists;
        }

        var startDate = DateTime.Now.AddDays(1).Date;
        var expectedEndDate = startDate.AddDays(rentalType.Days).Date;

        var rental = new Rental(motorcycleId, delivererId, rentalType.Id, startDate, expectedEndDate);

        return rental;
    }
}
