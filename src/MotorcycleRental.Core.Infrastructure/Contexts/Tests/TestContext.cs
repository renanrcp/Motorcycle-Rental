using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MotorcycleRental.Core.Infrastructure.Contexts.Tests;

internal delegate Task SeedDelegate(TestContext dbContext);

internal abstract class TestContext(DbContextOptions options, IPublisher publisher)
    : ApplicationContext(options, publisher)
{
    protected IDbContextTransaction? _transaction;


    protected static async Task<TContext> InternalCreateAsync<TContext>(TContext context, SeedDelegate? seedDelegate = null)
        where TContext : TestContext
    {
        await context.Database.OpenConnectionAsync();
        await context.Database.EnsureCreatedAsync();

        var transaction = await context.Database.BeginTransactionAsync();
        context._transaction = transaction;

        if (seedDelegate != null)
        {
            await seedDelegate(context);
            await context.SaveChangesAsync();
        }

        return context;
    }

    public override void Dispose()
    {
        base.Dispose();

        _transaction?.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();

        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }
    }
}
