using MongoDB.Bson;
using MotorcycleRental.Core.Application.Abstractions;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Core.Application.Events;

public record ExceptionEvent : ApplicationEvent
{
    public ExceptionEvent(ObjectId Id, Exception exception) : base(Id)
    {
        ArgumentNullException.ThrowIfNull(exception);

        Exception = exception;
        Message = exception.Message;
        StackTrace = exception.StackTrace;
    }

    [JsonIgnore]
    public Exception Exception { get; }

    public string Message { get; }

    public string? StackTrace { get; }
}

