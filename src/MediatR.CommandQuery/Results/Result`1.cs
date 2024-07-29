using System.Collections.Immutable;
using System.ComponentModel;

namespace MediatR.CommandQuery.Results;

/// <summary>Represents a result.</summary>
/// <typeparam name="TValue">The type of the value in the result.</typeparam>
[ImmutableObject(true)]
public sealed class Result<TValue> : IResult<TValue>
{

    public static readonly Result<TValue> FailedResult = new(Results.Error.Empty);

    private readonly ImmutableArray<IError> _errors;
    private readonly TValue? _value;


    public Result(TValue value)
    {
        _value = value;
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

    /// <inheritdoc />
    public TValue Value
    {
        get
        {
            if (IsFailed)
                throw new InvalidOperationException($"{nameof(Result)} is failed. {nameof(Value)} is not set.");

            return _value!;
        }
    }


    /// <summary>Creates a success result with the specified value.</summary>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a success result with the specified value.</returns>
    public static Result<TValue> Ok(TValue value)
    {
        return new Result<TValue>(value);
    }


    /// <summary>Creates a failed result.</summary>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result.</returns>
    public static Result<TValue> Fail()
    {
        return FailedResult;
    }

    /// <summary>Creates a failed result with the given error message.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    public static Result<TValue> Fail(string errorMessage)
    {
        var error = new Error(errorMessage);
        return Fail(error);
    }

    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    public static Result<TValue> Fail(string errorMessage, (string Key, object Value) metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Fail(error);
    }

    /// <summary>Creates a failed result with the given error message and metadata.</summary>
    /// <param name="errorMessage">The error message associated with the failure.</param>
    /// <param name="metadata">The metadata associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error message.</returns>
    public static Result<TValue> Fail(string errorMessage, IReadOnlyDictionary<string, object> metadata)
    {
        var error = new Error(errorMessage, metadata);
        return Fail(error);
    }

    /// <summary>Creates a failed result with the given error.</summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified error.</returns>
    public static Result<TValue> Fail(IError error)
    {
        return new Result<TValue>(error);
    }

    /// <summary>Creates a failed result with the given errors.</summary>
    /// <param name="errors">A collection of errors associated with the failure.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> representing a failed result with the specified errors.</returns>
    public static Result<TValue> Fail(IEnumerable<IError> errors)
    {
        return new Result<TValue>(errors);
    }


    /// <summary>
    /// Converts <see cref="Result{TValue}"/> to <typeparamref name="TValue"/>
    /// </summary>
    /// <param name="value">The value to convert</param>
    public static implicit operator TValue(Result<TValue> value) => value.Value;

    /// <summary>
    /// Converts <typeparamref name="TValue"/> to <see cref="Result{TValue}"/>
    /// </summary>
    /// <param name="value">The value to convert</param>
    public static explicit operator Result<TValue>(TValue value) => Result<TValue>.Ok(value);

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
