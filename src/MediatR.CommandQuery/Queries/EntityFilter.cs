using System.Text.Json.Serialization;

using MediatR.CommandQuery.Converters;

namespace MediatR.CommandQuery.Queries;

[JsonConverter(typeof(EntityFilterConverter))]
public class EntityFilter
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("value")]
    public object? Value { get; set; }

    [JsonPropertyName("logic")]
    public string? Logic { get; set; }

    [JsonPropertyName("filters")]
    public List<EntityFilter>? Filters { get; set; }


    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Name);
        hash.Add(Operator);
        hash.Add(Value);
        hash.Add(Logic);

        if (Filters == null)
            return hash.ToHashCode();

        foreach (var filter in Filters)
            hash.Add(filter.GetHashCode());

        return hash.ToHashCode();
    }
}
