using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models;

/// <summary>
/// A read model base <c>class</c>
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="EntityIdentifierModel{TKey}" />
/// <seealso cref="ITrackCreated" />
/// <seealso cref="ITrackUpdated" />
/// <seealso cref="ITrackConcurrency" />
public class EntityReadModel<TKey> : EntityIdentifierModel<TKey>, ITrackCreated, ITrackUpdated, ITrackConcurrency
{
    /// <inheritdoc />
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public string? CreatedBy { get; set; }

    /// <inheritdoc />
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    public string? UpdatedBy { get; set; }

    /// <inheritdoc />
    public long RowVersion { get; set; }
}
