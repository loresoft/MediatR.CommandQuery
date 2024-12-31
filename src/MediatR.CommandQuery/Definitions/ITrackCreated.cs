namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> indicating the implemented type supports tracking creating audit fields
/// </summary>
public interface ITrackCreated
{
    /// <summary>
    /// Gets or sets the Coordinated Universal Time (UTC) date and time the entity was created.
    /// </summary>
    /// <value>
    /// The Coordinated Universal Time (UTC) date and time the entity was created.
    /// </value>
    DateTimeOffset Created { get; set; }

    /// <summary>
    /// Gets or sets the user that created this entity.
    /// </summary>
    /// <value>
    /// The user that created this entity.
    /// </value>
    string? CreatedBy { get; set; }
}
