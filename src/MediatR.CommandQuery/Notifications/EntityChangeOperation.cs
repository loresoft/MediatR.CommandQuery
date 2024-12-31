namespace MediatR.CommandQuery.Notifications;

/// <summary>
/// The type of entity change operations
/// </summary>
public enum EntityChangeOperation
{
    /// <summary>
    /// The entity was created
    /// </summary>
    Created,
    /// <summary>
    /// The entity was updated
    /// </summary>
    Updated,
    /// <summary>
    /// The entity was deleted
    /// </summary>
    Deleted,
}
