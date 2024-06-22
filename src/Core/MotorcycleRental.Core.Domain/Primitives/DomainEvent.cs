using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotorcycleRental.Core.Domain.Primitives;

public record DomainEvent : INotification
{
    public DomainEvent(ObjectId id)
    {
        Id = id;
    }

    [BsonId]
    public ObjectId Id { get; }
}
