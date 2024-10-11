using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Queries;

public record EntityIdentifiersQuery<TKey, TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
{
    public EntityIdentifiersQuery(ClaimsPrincipal? principal, [NotNull] IEnumerable<TKey> ids)
        : base(principal)
    {
        if (ids is null)
            throw new ArgumentNullException(nameof(ids));

        Ids = ids.ToList();
    }

    [NotNull]
    public IReadOnlyCollection<TKey> Ids { get; }

    public override string GetCacheKey()
    {
        var hash = new HashCode();

        foreach (var id in Ids)
            hash.Add(id);

        return $"{typeof(TReadModel).FullName}-{hash.ToHashCode()}";
    }
}
