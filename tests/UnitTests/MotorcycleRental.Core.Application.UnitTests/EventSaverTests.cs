using Moq;
using MongoDB.Driver;
using MotorcycleRental.Core.Application.Implementations;
using MotorcycleRental.Core.Domain.Primitives;
using MotorcycleRental.Core.Application.Abstractions;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;

namespace MotorcycleRental.Core.Application.UnitTests;

public class EventSaverTests
{
    private readonly Mock<IMongoClient> _mockMongoClient;
    private readonly Mock<IMongoDatabase> _mockMongoDatabase;
    private readonly Mock<IMongoCollection<DomainEvent>> _mockDomainEventCollection;
    private readonly Mock<IMongoCollection<ApplicationEvent>> _mockApplicationEventCollection;
    private readonly Mock<IHostEnvironment> _mockHostEnvironment;
    private readonly EventSaver _eventSaver;

    public EventSaverTests()
    {
        _mockMongoClient = new Mock<IMongoClient>();
        _mockMongoDatabase = new Mock<IMongoDatabase>();
        _mockDomainEventCollection = new Mock<IMongoCollection<DomainEvent>>();
        _mockApplicationEventCollection = new Mock<IMongoCollection<ApplicationEvent>>();
        _mockHostEnvironment = new Mock<IHostEnvironment>();

        _mockHostEnvironment.Setup(x => x.ApplicationName).Returns("test");

        _mockMongoClient
            .Setup(client => client.GetDatabase(It.IsAny<string>(), null))
            .Returns(_mockMongoDatabase.Object);

        _mockMongoDatabase
            .Setup(db => db.GetCollection<DomainEvent>(It.IsAny<string>(), null))
            .Returns(_mockDomainEventCollection.Object);

        _mockMongoDatabase
            .Setup(db => db.GetCollection<ApplicationEvent>(It.IsAny<string>(), null))
            .Returns(_mockApplicationEventCollection.Object);

        _eventSaver = new EventSaver(_mockMongoClient.Object, _mockHostEnvironment.Object);
    }

    [Fact]
    public async Task SaveEventAsync_DomainEvent_ShouldInsertDomainEvent()
    {
        // Arrange
        var domainEvent = new DomainEvent(ObjectId.GenerateNewId());

        // Act
        await _eventSaver.SaveEventAsync(domainEvent);

        // Assert
        _mockDomainEventCollection.Verify(
            collection => collection.InsertOneAsync(domainEvent, null, default),
            Times.Once);
    }

    [Fact]
    public async Task SaveEventAsync_ApplicationEvent_ShouldInsertApplicationEvent()
    {
        // Arrange
        var applicationEvent = new ApplicationEvent(ObjectId.GenerateNewId());

        // Act
        await _eventSaver.SaveEventAsync(applicationEvent);

        // Assert
        _mockApplicationEventCollection.Verify(
            collection => collection.InsertOneAsync(applicationEvent, null, default),
            Times.Once);
    }
}
