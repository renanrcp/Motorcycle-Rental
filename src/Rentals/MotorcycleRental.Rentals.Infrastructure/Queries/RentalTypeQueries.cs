using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Queries;

public static class RentalTypeQueries
{
    public static IQueryable<RentalType> WhereId(this IQueryable<RentalType> query, int id)
    {
        return query.Where(x => x.Id == id);
    }
}
