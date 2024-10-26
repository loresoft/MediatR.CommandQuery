using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries;

public class EntityQuery : EntitySelect
{
    [JsonConstructor]
    public EntityQuery()
    {
        Page = 1;
        PageSize = 20;
    }

    public EntityQuery(string? query, int page, int pageSize, string? sort)
        : base(query, sort)
    {
        Page = page;
        PageSize = pageSize;
    }

    public EntityQuery(EntityFilter? filter, int page = 1, int pageSize = 20)
        : this(filter, Enumerable.Empty<EntitySort>(), page, pageSize)
    {
    }

    public EntityQuery(EntityFilter? filter, EntitySort? sort, int page = 1, int pageSize = 20)
        : this(filter, (sort != null) ? [sort] : null, page, pageSize)
    {
    }

    public EntityQuery(EntityFilter? filter, IEnumerable<EntitySort>? sort, int page = 1, int pageSize = 20)
        : base(filter, sort)
    {
        Page = page;
        PageSize = pageSize;
    }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }


    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Page, PageSize);
    }
}
