using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries;

public class EntityContinuationResult<TReadModel>
{
    [JsonPropertyName("previousToken")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PreviousToken { get; set; }

    [JsonPropertyName("continuationToken")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ContinuationToken { get; set; }

    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<TReadModel>? Data { get; set; }
}
