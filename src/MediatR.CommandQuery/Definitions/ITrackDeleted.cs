namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> indicating the implemented type supports soft delete
/// </summary>
public interface ITrackDeleted
{
    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
    /// </value>
    bool IsDeleted { get; set; }
}
