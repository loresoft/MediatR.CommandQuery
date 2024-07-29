namespace MediatR.CommandQuery.Results;

/// <summary>
/// Represents an error with a message and associated metadata.
/// </summary>
public class Error : IError
{
    public static Error Empty { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Error" /> class.
    /// </summary>
    public Error() : this("")
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified error message.</summary>
    /// <param name="message">The error message.</param>
    public Error(string message)
    {
        Message = message;
        Metadata = new Dictionary<string, object>();
    }

    /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified metadata.</summary>
    /// <param name="metadata">The metadata associated with the error.</param>
    public Error((string Key, object Value) metadata) : this("", metadata)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified error message and metadata.</summary>
    /// <param name="message">The error message.</param>
    /// <param name="metadata">The metadata associated with the error.</param>
    public Error(string message, (string Key, object Value) metadata)
    {
        Message = message;

        var builder = new Dictionary<string, object>();
        builder.Add(metadata.Key, metadata.Value);
        Metadata = builder;
    }

    /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified metadata.</summary>
    /// <param name="metadata">The metadata associated with the error.</param>
    public Error(IReadOnlyDictionary<string, object> metadata) : this("", metadata)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified error message and metadata.</summary>
    /// <param name="message">The error message.</param>
    /// <param name="metadata">The metadata associated with the error.</param>
    public Error(string message, IReadOnlyDictionary<string, object> metadata)
    {
        Message = message;
        Metadata = metadata;
    }

    /// <inheritdoc />
    public string Message { get; }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, object> Metadata { get; }
}
