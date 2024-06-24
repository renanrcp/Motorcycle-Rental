using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Rentals.Domain.Entities;

public class RentalType
{
    private RentalType(int days, decimal cost)
    {
        Days = days;
        Cost = cost;
    }

    public int Id { get; private set; }

    public int Days { get; private set; }

    public decimal Cost { get; private set; }

    public static Result<RentalType> Create(int days, decimal cost)
    {
        var rentalType = new RentalType(days, cost);

        return rentalType;
    }
}
