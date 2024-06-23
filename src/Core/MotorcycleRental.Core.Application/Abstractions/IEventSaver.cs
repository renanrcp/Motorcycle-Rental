namespace MotorcycleRental.Core.Application.Abstractions;

public interface IApplicationEventSaver
{
    Task SaveEventAsync(ApplicationEvent applicationEvent, CancellationToken cancellationToken = default);
}
