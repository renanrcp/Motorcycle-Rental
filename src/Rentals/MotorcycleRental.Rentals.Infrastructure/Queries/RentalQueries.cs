using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Queries;

public static class RentalQueries
{
    public static IQueryable<Rental> WhereId(this IQueryable<Rental> query, int id)
    {
        return query.Where(x => x.Id == id);
    }

    public static IQueryable<Rental> WhereMotorycleId(this IQueryable<Rental> query, int motorcycleId)
    {
        return query.Where(x => x.MotorcycleId == motorcycleId);
    }

    public static IQueryable<Rental> WhereDelivererId(this IQueryable<Rental> query, int delivererId)
    {
        return query.Where(x => x.DelivererId == delivererId);
    }
}
