using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Migrator.Contexts;

namespace MotorcycleRental.Core.Migrator.Services;

public class MigrationService(IServiceScopeFactory serviceScopeFactory) : IHostedLifecycleService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private CancellationTokenSource? _cts;

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        await dbContext.Database.MigrateAsync(_cts.Token);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel(false);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _cts?.Dispose();
        return Task.CompletedTask;
    }
}
