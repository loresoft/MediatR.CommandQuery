namespace MediatR.CommandQuery.Definitions;

/// <summary>
/// An <c>interface</c> indicating the implemented type supports tracking update audit fields
/// </summary>
public interface ITrackUpdated
{
    /// <summary>
    /// Gets or sets the Coordinated Universal Time (UTC) date and time the entity was last updated.
    /// </summary>
    /// <value>
    /// The Coordinated Universal Time (UTC) date and time the entity was last updated.
    /// </value>
    DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Gets or sets the user that last updated this entity.
    /// </summary>
    /// <value>
    /// The user that last updated this entity.
    /// </value>
    string? UpdatedBy { get; set; }
}
