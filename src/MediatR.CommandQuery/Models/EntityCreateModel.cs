using System.Text.Json.Serialization;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models;

/// <summary>
/// A create model base <c>class</c>
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="EntityIdentifierModel{TKey}" />
/// <seealso cref="ITrackCreated" />
/// <seealso cref="ITrackUpdated" />
public class EntityCreateModel<TKey> : EntityIdentifierModel<TKey>, ITrackCreated, ITrackUpdated
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
}
