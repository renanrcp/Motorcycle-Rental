using MotorcycleRental.Motorcycles.Domain.Entities;

namespace MotorcycleRental.Motorcycles.Infrastructure.Queries.Motorcycles;

public static class MotorcycleQueries
{
    public static IQueryable<Motorcycle> WhereLicensePlate(this IQueryable<Motorcycle> query, string licensePlate)
    {
        return query.Where(x => x.LicensePlate.StartsWith(licensePlate));
    }
}
