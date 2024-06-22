using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotorcycleRental.Core.Application.Abstractions;

public record ApplicationEvent : INotification
{
    public ApplicationEvent(ObjectId id)
    {
        Id = id;
    }

    [BsonId]
    public ObjectId Id { get; }
}
