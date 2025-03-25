using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using EntityChange;

namespace MediatR.CommandQuery.Audit;

/// <summary>
/// An audit record of changes for an entity
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
public class AuditRecord<TKey>
{
    /// <summary>
    /// Gets or sets the key for the entity.
    /// </summary>
    /// <value>
    /// The key for the entity.
    /// </value>
    [NotNull]
    [JsonPropertyName("key")]
    public TKey Key { get; set; } = default!;

    /// <summary>
    /// Gets or sets the entity name.
    /// </summary>
    /// <value>
    /// The entity name.
    /// </value>
    [JsonPropertyName("entity")]
    public string Entity { get; set; } = null!;

    /// <summary>
    /// Gets or sets the entity display name.
    /// </summary>
    /// <value>
    /// The entity display name.
    /// </value>
    [JsonPropertyName("displayName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the description of the entity.
    /// </summary>
    /// <value>
    /// The description of the entity.
    /// </value>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the date the change activity occurred.
    /// </summary>
    /// <value>
    /// The date the change activity occurred.
    /// </value>
    [JsonPropertyName("activityDate")]
    public DateTimeOffset ActivityDate { get; set; }

    /// <summary>
    /// Gets or sets the user name that initiated the activity.
    /// </summary>
    /// <value>
    /// The user name that initiated the activity.
    /// </value>
    [JsonPropertyName("activityBy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ActivityBy { get; set; }

    /// <summary>
    /// Gets or sets the type of operation.
    /// </summary>
    /// <value>
    /// The type of operation.
    /// </value>
    [JsonPropertyName("operation")]
    [JsonConverter(typeof(JsonStringEnumConverter<AuditOperation>))]
    public AuditOperation Operation { get; set; }

    /// <summary>
    /// Gets or sets the list of changes.
    /// </summary>
    /// <value>
    /// The list of changes.
    /// </value>
    [JsonPropertyName("changes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<ChangeRecord>? Changes { get; set; }
}
