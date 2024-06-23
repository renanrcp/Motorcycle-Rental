using MongoDB.Bson.Serialization.Attributes;
using MotorcycleRental.Core.Application.Abstractions;

namespace MotorcycleRental.Core.Application.Events;

public record ExceptionEvent : ApplicationEvent
{
    private readonly Exception _exception;

    public ExceptionEvent(Exception exception)
    {
        _exception = exception;
        Message = exception.Message;
        StackTrace = exception.StackTrace;
    }

    public string Message { get; private set; }

    public string? StackTrace { get; private set; }

    public Exception GetException()
    {
        return _exception;
    }
}

