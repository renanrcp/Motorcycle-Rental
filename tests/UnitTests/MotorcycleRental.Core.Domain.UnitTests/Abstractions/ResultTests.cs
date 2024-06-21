using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Domain.UnitTests.Abstractions;

public class ResultTests
{
    [Fact]
    public void SuccessResult_ShouldBeSuccessful()
    {
        var result = Result.Success;

        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
    }

    [Fact]
    public void ErrorResult_ShouldBeFaulted()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFaulted);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Result_WithNullError_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() => new Result(null!));
    }

    [Fact]
    public void Result_WithInvalidError_ShouldThrow()
    {
        var invalidError = new Error(string.Empty, "Invalid Error");

        Assert.Throws<ArgumentException>(() => new Result(invalidError));
    }

    [Fact]
    public void Match_ShouldReturnOnSuccessFunctionResult()
    {
        var result = Result.Success;
        var output = result.Match(() => "Success", error => "Failure");

        Assert.Equal("Success", output);
    }

    [Fact]
    public void Match_ShouldReturnOnFailedFunctionResult()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result(error);
        var output = result.Match(() => "Success", error => "Failure");

        Assert.Equal("Failure", output);
    }

    [Fact]
    public void IfFail_ShouldExecuteOnError()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result(error);

        var executed = false;
        result.IfFail(e => executed = true);

        Assert.True(executed);
    }

    [Fact]
    public void IfSuccess_ShouldExecuteOnSuccess()
    {
        var result = Result.Success;

        var executed = false;
        result.IfSuccess(() => executed = true);

        Assert.True(executed);
    }

    [Fact]
    public void Map_ShouldReturnMappedResultOnSuccess()
    {
        var result = Result.Success;
        var mappedResult = result.Map(() => "Mapped");

        Assert.True(mappedResult.IsSuccess);
        Assert.Equal("Mapped", mappedResult.Value);
    }

    [Fact]
    public async Task MapAsync_ShouldReturnMappedResultOnSuccess()
    {
        var result = Result.Success;
        var mappedResult = await result.MapAsync(() => Task.FromResult("Mapped"));

        Assert.True(mappedResult.IsSuccess);
        Assert.Equal("Mapped", mappedResult.Value);
    }
}

public class ResultTTests
{
    [Fact]
    public void SuccessResultT_ShouldBeSuccessful()
    {
        var result = new Result<string>("Success");

        Assert.True(result.IsSuccess);
        Assert.Equal("Success", result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void ErrorResultT_ShouldBeFaulted()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result<string>(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFaulted);
        Assert.Equal(error, result.Error);
        Assert.Null(result.Value);
    }

    [Fact]
    public void ResultT_WithNullError_ShouldThrow()
    {
        Assert.Throws<ArgumentNullException>(() => new Result<string>((Error)null!));
    }

    [Fact]
    public void ResultT_WithInvalidError_ShouldThrow()
    {
        var invalidError = new Error(string.Empty, "Invalid Error");
        Assert.Throws<ArgumentException>(() => new Result<string>(invalidError));
    }

    [Fact]
    public void MatchT_ShouldReturnOnSuccessFunctionResult()
    {
        var result = new Result<string>("Success");
        var output = result.Match(value => $"Success: {value}", error => "Failure");

        Assert.Equal("Success: Success", output);
    }

    [Fact]
    public void MatchT_ShouldReturnOnFailedFunctionResult()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result<string>(error);
        var output = result.Match(value => $"Success: {value}", error => "Failure");

        Assert.Equal("Failure", output);
    }

    [Fact]
    public void IfFail_ShouldReturnDefaultValueOnError()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result<string>(error);

        var output = result.IfFail("DefaultValue");

        Assert.Equal("DefaultValue", output);
    }

    [Fact]
    public void IfFail_ShouldExecuteOnError()
    {
        var error = new Error("E001", "Test Error");
        var result = new Result<string>(error);

        var output = result.IfFail(err => $"Error: {err.Description}");

        Assert.Equal("Error: Test Error", output);
    }

    [Fact]
    public void IfSuccessT_ShouldExecuteOnSuccess()
    {
        var result = new Result<string>("Success");

        var executed = false;
        result.IfSuccess(value => executed = true);

        Assert.True(executed);
    }

    [Fact]
    public void MapT_ShouldReturnMappedResultOnSuccess()
    {
        var result = new Result<string>("Success");
        var mappedResult = result.Map(value => value.Length);

        Assert.True(mappedResult.IsSuccess);
        Assert.Equal(7, mappedResult.Value);
    }

    [Fact]
    public async Task MapAsyncT_ShouldReturnMappedResultOnSuccess()
    {
        var result = new Result<string>("Success");
        var mappedResult = await result.MapAsync(value => Task.FromResult(value.Length));

        Assert.True(mappedResult.IsSuccess);
        Assert.Equal(7, mappedResult.Value);
    }
}
