using System.Text.Json.Serialization;

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
    [JsonPropertyName("created")]
    [JsonPropertyOrder(9990)]
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    [JsonPropertyName("createdBy")]
    [JsonPropertyOrder(9991)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("updated")]
    [JsonPropertyOrder(9992)]
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

    /// <inheritdoc />
    [JsonPropertyName("updatedBy")]
    [JsonPropertyOrder(9993)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UpdatedBy { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("rowVersion")]
    [JsonPropertyOrder(9999)]
    public long RowVersion { get; set; }
}
