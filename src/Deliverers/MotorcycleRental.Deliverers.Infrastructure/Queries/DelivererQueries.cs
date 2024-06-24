using MotorcycleRental.Deliverers.Domain.Entities;

namespace MotorcycleRental.Deliverers.Infrastructure.Queries;

public static class DelivererQueries
{
    public static IQueryable<Deliverer> WhereCnh(this IQueryable<Deliverer> query, string cnh)
    {
        return query.Where(x => x.Cnh.Equals(cnh));
    }

    public static IQueryable<Deliverer> WhereCnpj(this IQueryable<Deliverer> query, string cnpj)
    {
        return query.Where(x => x.Cnpj.Equals(cnpj));
    }
}
