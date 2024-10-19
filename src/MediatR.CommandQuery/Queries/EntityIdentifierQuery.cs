using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Queries;

public record EntityIdentifierQuery<TKey, TReadModel> : CacheableQueryBase<TReadModel>
{

    public EntityIdentifierQuery(ClaimsPrincipal? principal, [NotNull] TKey id)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }


    public override string GetCacheKey()
        => CacheTagger.GetKey<TReadModel, TKey>(CacheTagger.Buckets.Identifier, Id);

    public override string? GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
