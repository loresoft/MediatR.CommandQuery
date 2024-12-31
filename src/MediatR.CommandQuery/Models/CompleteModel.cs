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
    public bool Successful { get; set; }

    /// <summary>
    /// Gets or sets the operation result message.
    /// </summary>
    /// <value>
    /// The operation result message.
    /// </value>
    public string? Message { get; set; }
}
