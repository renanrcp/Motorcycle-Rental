using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Primitives;

namespace MotorcycleRental.Core.Application.Implementations;

public class EventSaver : IApplicationEventSaver, IDomainEventSaver
{
    private readonly IMongoClient _mongoClient;

    private readonly IMongoDatabase _mongoDatabase;

    private readonly IMongoCollection<DomainEvent> _domainEvents;

    private readonly IMongoCollection<ApplicationEvent> _applicationEvents;

    public EventSaver(IMongoClient mongoClient, IHostEnvironment hostEnvironment)
    {
        _mongoClient = mongoClient;

        var applicationName = hostEnvironment.ApplicationName
                                                .Replace(".", string.Empty)
                                                .Replace("MotorcycleRental", string.Empty)
                                                .Replace("Presentation", string.Empty)
                                                .ToLower();

        _mongoDatabase = _mongoClient.GetDatabase($"motorcycle_{applicationName}_events");
        _domainEvents = _mongoDatabase.GetCollection<DomainEvent>("domain_events");
        _applicationEvents = _mongoDatabase.GetCollection<ApplicationEvent>("application_events");
    }



    public Task SaveEventAsync(DomainEvent domainEvent)
    {
        return _domainEvents.InsertOneAsync(domainEvent);
    }

    public Task SaveEventAsync(ApplicationEvent applicationEvent)
    {
        return _applicationEvents.InsertOneAsync(applicationEvent);
    }
}
