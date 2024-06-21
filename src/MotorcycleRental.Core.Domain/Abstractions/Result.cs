namespace MotorcycleRental.Core.Domain.Abstractions;

public class Result
{
    public static readonly Result Success = new();

    public Result()
    {
        IsSuccess = true;
        Error = null;
    }

    public Result(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        if (error.Code.Equals(Error.None.Code) || string.IsNullOrWhiteSpace(error.Code))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = false;
        Error = error;
    }

    public virtual Error? Error { get; }

    public virtual bool IsSuccess { get; }

    public virtual bool IsFaulted => !IsSuccess;

    public virtual R Match<R>(Func<R> onSuccess, Func<Error, R> onFailed)
    {
        return IsSuccess
            ? onSuccess()
            : onFailed(Error!);
    }

    public virtual Result IfFail(Action<Error> func)
    {
        if (IsFaulted)
        {
            func(Error!);
        }

        return this;
    }

    public virtual Result IfSuccess(Action func)
    {
        if (IsSuccess)
            func();

        return this;
    }

    public Result<B> Map<B>(Func<B> func)
    {
        return IsFaulted
            ? new Result<B>(Error!)
            : new Result<B>(func());
    }

    public async Task<Result<B>> MapAsync<B>(Func<Task<B>> func)
    {
        return IsFaulted
            ? new Result<B>(Error!)
            : new Result<B>(await func());
    }

    public static implicit operator Result(Error error)
        => new(error);
}

public class Result<T> : Result
{
    public Result(T value) : base()
    {
        Value = value;
    }

    public Result(Error error) : base(error)
    {
        Value = default;
    }

    public virtual T? Value { get; }

    public virtual R Match<R>(Func<T, R> onSuccess, Func<Error, R> onFailed)
    {
        return IsSuccess
            ? onSuccess(Value!)
            : onFailed(Error!);
    }

    public virtual T IfFail(T defaultValue)
    {
        return IsSuccess
            ? Value!
            : defaultValue;
    }

    public virtual T IfFail(Func<Error, T> func)
    {
        return IsFaulted
            ? func(Error!)
            : Value!;
    }

    public virtual Result<T> IfSuccess(Action<T> func)
    {
        if (IsSuccess)
            func(Value!);

        return this;
    }

    public virtual Result<B> Map<B>(Func<T, B> func)
    {
        return IsFaulted
            ? new Result<B>(Error!)
            : new Result<B>(func(Value!));
    }

    public virtual async Task<Result<B>> MapAsync<B>(Func<T, Task<B>> func)
    {
        return IsFaulted
            ? new Result<B>(Error!)
            : new Result<B>(await func(Value!));
    }

    public static implicit operator Result<T>(T value)
        => new(value);

    public static implicit operator Result<T>(Error error)
        => new(error);
}
