using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Infrastructure.Contexts.Tests;

internal abstract class InMemoryTestContext(DbContextOptions options, IDomainEventSaver domainEventSaver)
    : TestContext(options, domainEventSaver)
{
    protected static DbContextOptions<TContext> InternalCreateOptions<TContext>()
        where TContext : InMemoryTestContext
    {
        var baseDbName = typeof(TContext).Name.Replace("TestContext", string.Empty);
        var dbName = $"{baseDbName}-{Guid.NewGuid()}";

        var options = new DbContextOptionsBuilder<TContext>()
                    .UseInMemoryDatabase(dbName)
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;

        return options;
    }
}
