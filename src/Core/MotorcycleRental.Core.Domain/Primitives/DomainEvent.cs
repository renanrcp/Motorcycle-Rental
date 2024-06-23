using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotorcycleRental.Core.Domain.Primitives;

public record DomainEvent : INotification
{
    [BsonId]
    public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();

    public DateTime CreatedDate { get; private set; } = DateTime.Now;
}
