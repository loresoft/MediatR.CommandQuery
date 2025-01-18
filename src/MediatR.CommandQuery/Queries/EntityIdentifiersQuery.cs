using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Queries;

public record EntityIdentifiersQuery<TKey, TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
{
    public EntityIdentifiersQuery(ClaimsPrincipal? principal, [NotNull] IReadOnlyCollection<TKey> ids)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(ids);

        Ids = ids;
    }

    [NotNull]
    public IReadOnlyCollection<TKey> Ids { get; }

    public override string GetCacheKey()
    {
        var hash = new HashCode();

        foreach (var id in Ids)
            hash.Add(id);

        return CacheTagger.GetKey<TReadModel, int>(CacheTagger.Buckets.Identifiers, hash.ToHashCode());
    }

    public override string? GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
