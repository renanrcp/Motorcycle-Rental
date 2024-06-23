using MediatR;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Core.Application.Implementations;

public class EventSaver : IApplicationEventSaver, IDomainEventSaver
{
    private readonly IMediator _mediator;

    private readonly IMongoClient _mongoClient;

    private readonly IMongoDatabase _mongoDatabase;

    private readonly IMongoCollection<DomainEvent> _domainEvents;

    private readonly IMongoCollection<ApplicationEvent> _applicationEvents;

    public EventSaver(IMongoClient mongoClient, IMediator mediator, IHostEnvironment hostEnvironment)
    {
        _mongoClient = mongoClient;
        _mediator = mediator;

        var applicationName = hostEnvironment.ApplicationName
                                                .Replace(".", string.Empty)
                                                .Replace("MotorcycleRental", string.Empty)
                                                .Replace("Presentation", string.Empty)
                                                .ToLower();

        _mongoDatabase = _mongoClient.GetDatabase($"motorcycle_{applicationName}_events");
        _domainEvents = _mongoDatabase.GetCollection<DomainEvent>("domain_events");
        _applicationEvents = _mongoDatabase.GetCollection<ApplicationEvent>("application_events");
    }



    public async Task SaveEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(domainEvent, cancellationToken);
        await _domainEvents.InsertOneAsync(domainEvent, options: null, cancellationToken);
    }

    public async Task SaveEventAsync(ApplicationEvent applicationEvent, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(applicationEvent, cancellationToken);
        await _applicationEvents.InsertOneAsync(applicationEvent, options: null, cancellationToken);
    }
}
