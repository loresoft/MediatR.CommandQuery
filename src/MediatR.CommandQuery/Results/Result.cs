using System.Collections.Immutable;
using System.ComponentModel;

namespace MediatR.CommandQuery.Results;

/// <summary>Represents a result.</summary>
[ImmutableObject(true)]
public sealed class Result : IResult
{

    public static readonly Result OkResult = new();
    public static readonly Result FailedResult = new(Results.Error.Empty);

    private readonly ImmutableArray<IError> _errors;

    public Result()
    {
        _errors = ImmutableArray<IError>.Empty;
    }

    public Result(IError error)
    {
        _errors = ImmutableArray.Create(error);
    }

    public Result(IEnumerable<IError> errors)
    {
        _errors = errors.ToImmutableArray();
    }


    /// <inheritdoc />
    public bool IsSuccess => _errors.Length == 0;

    /// <inheritdoc />
    public bool IsFailed => _errors.Length != 0;

    /// <inheritdoc />
    public IReadOnlyCollection<IError> Errors => _errors;


    /// <summary>Creates a success result.</summary>
    /// <returns>A new instance of <see cref="Result" /> representing a success result with the specified value.</returns>
    public static Result Ok()
    {
        return OkResult;
    }

    /// <summary>Creates a success result with the specified value.</summary>
    /// <param name="value">The value to include in the result.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a success result with the specified value.</returns>
    public static Result<TValue> Ok<TValue>(TValue value)
    {
        return Result<TValue>.Ok(value);
    }


    /// <summary>Creates a failed result.</summary>
    /// <returns>A new instance of <see cref="Result" /> representing a failed result.</returns>
    public static Result Fail()
    {
        return FailedResult;
    }

    /// <summary>Creates a failed result with the given error message.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result" /> representing a failed result with the specified error message.</returns>
    public static Result Fail(string errorMessage)
    {
        var error = new Error(errorMessage);
        return Fail(error);
    }

    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result" /> representing a failed result with the specified error message and metadata.</returns>
    public static Result Fail(string errorMessage, (string Key, object Value) metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Fail(error);
    }

    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result" /> representing a failed result with the specified error message and metadata.</returns>
    public static Result Fail(string errorMessage, IReadOnlyDictionary<string, object> metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Fail(error);
    }

    /// <summary>Creates a failed result with the given error.</summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result" /> representing a failed result with the specified error.</returns>
    public static Result Fail(IError error)
    {
        return new Result(error);
    }

    /// <summary>Creates a failed result with the given errors.</summary>
    /// <param name="errors">A collection of errors associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result" /> representing a failed result with the specified errors.</returns>
    public static Result Fail(IEnumerable<IError> errors)
    {
        return new Result(errors);
    }


    /// <summary>Creates a failed result.</summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result.</returns>
    public static Result<TValue> Fail<TValue>()
    {
        return Result<TValue>.Fail();
    }

    /// <summary>Creates a failed result with the given error message.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    public static Result<TValue> Fail<TValue>(string errorMessage)
    {
        var error = new Error(errorMessage);
        return Result<TValue>.Fail(error);
    }

    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message and metadata.</returns>
    public static Result<TValue> Fail<TValue>(string errorMessage, (string Key, object Value) metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Result<TValue>.Fail(error);
    }

    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message and metadata.</returns>
    public static Result<TValue> Fail<TValue>(string errorMessage, IReadOnlyDictionary<string, object> metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Result<TValue>.Fail(error);
    }

    /// <summary>Creates a failed result with the given error.</summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error.</returns>
    public static Result<TValue> Fail<TValue>(IError error)
    {
        return Result<TValue>.Fail(error);
    }

    /// <summary>Creates a failed result with the given errors.</summary>
    /// <param name="errors">A collection of errors associated with the failure.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified errors.</returns>
    public static Result<TValue> Fail<TValue>(IEnumerable<IError> errors)
    {
        return Result<TValue>.Fail(errors);
    }


    /// <inheritdoc />
    public bool HasError<TError>() where TError : IError
    {
        // Do not convert to LINQ, this creates unnecessary heap allocations.
        // For is the most efficient way to loop. It is the fastest and does not allocate.
        // ReSharper disable once ForCanBeConvertedToForeach
        // ReSharper disable once LoopCanBeConvertedToQuery
        for (var index = 0; index < _errors.Length; index++)
        {
            var error = _errors[index];
            if (error is TError)
                return true;
        }

        return false;
    }
}
