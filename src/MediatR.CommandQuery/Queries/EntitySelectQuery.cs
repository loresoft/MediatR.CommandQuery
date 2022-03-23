using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace MediatR.CommandQuery.Queries;

public class EntitySelectQuery<TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
{
    public EntitySelectQuery(IPrincipal principal)
        : this(principal, new EntitySelect())
    {
    }

    public EntitySelectQuery(IPrincipal principal, EntityFilter filter)
        : this(principal, new EntitySelect(filter))
    {
    }

    public EntitySelectQuery(IPrincipal principal, EntityFilter filter, EntitySort sort)
        : this(principal, filter, new[] { sort })
    {
    }

    public EntitySelectQuery(IPrincipal principal, EntityFilter filter, IEnumerable<EntitySort> sort)
        : this(principal, new EntitySelect(filter, sort))
    {

    }

    public EntitySelectQuery(IPrincipal principal, EntitySelect select)
        : base(principal)
    {
        Select = select ?? new EntitySelect();
    }

    public EntitySelect Select { get; }


    public override string GetCacheKey()
    {
        var hash = Select.GetHashCode();

        return $"{typeof(TReadModel).FullName}-Select-{hash}";
    }
}
