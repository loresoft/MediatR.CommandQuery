using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Models;

/// <summary>
/// Operation complete result model
/// </summary>
public class CompleteModel
{
    /// <summary>
    /// Gets or sets a value indicating whether operation was successful.
    /// </summary>
    /// <value>
    ///   <c>true</c> if was successful; otherwise, <c>false</c>.
    /// </value>
    [JsonPropertyName("successful")]
    public bool Successful { get; set; }

    /// <summary>
    /// Gets or sets the operation result message.
    /// </summary>
    /// <value>
    /// The operation result message.
    /// </value>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>
    /// Creates a success result with the specified message.
    /// </summary>
    /// <param name="message">The message associated with the success</param>
    /// <returns>A new instance representing a success result</returns>
    public static CompleteModel Success(string? message = null)
        => new() { Successful = true, Message = message };

    /// <summary>
    /// Creates a failed result with the given error message
    /// </summary>
    /// <param name="message">The error message associated with the failure.</param>
    /// <returns>A new instance representing a failed result with the specified error message</returns>
    public static CompleteModel Fail(string? message = null)
        => new() { Successful = false, Message = message };
}
