using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace MediatR.CommandQuery.Queries;

public class EntityIdentifierQuery<TKey, TReadModel> : CacheableQueryBase<TReadModel>
{

    public EntityIdentifierQuery(IPrincipal? principal, [NotNull] TKey id)
        : base(principal)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }


    public override string GetCacheKey()
    {
        return $"{typeof(TReadModel).FullName}-{Id}";
    }
}
