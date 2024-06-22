using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Application.Queries.Users;

public static class UserQueries
{
    public static IQueryable<User> WhereEmail(this IQueryable<User> query, string email)
    {
        return query
                .Where(x => x.Email.Equals(email));
    }
}
