using MediatR;

namespace MotorcycleRental.Core.Infrastructure.Contexts.Tests;

internal class TestPublisher : IPublisher
{
    private readonly List<object> _notifications = [];

    public IReadOnlyList<object> Notifications => _notifications;

    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        _notifications.Add(notification);

        return Task.CompletedTask;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        _notifications.Add(notification);

        return Task.CompletedTask;
    }
}
