using System.Text.Json.Serialization;

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
