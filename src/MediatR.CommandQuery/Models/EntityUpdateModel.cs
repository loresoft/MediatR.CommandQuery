using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models;

/// <summary>
/// An update model base <c>class</c>
/// </summary>
/// <seealso cref="MediatR.CommandQuery.Definitions.ITrackUpdated" />
/// <seealso cref="MediatR.CommandQuery.Definitions.ITrackConcurrency" />
public class EntityUpdateModel : ITrackUpdated, ITrackConcurrency
{
    /// <inheritdoc />
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public string? UpdatedBy { get; set; }

    /// <inheritdoc />
    public long RowVersion { get; set; }
}
