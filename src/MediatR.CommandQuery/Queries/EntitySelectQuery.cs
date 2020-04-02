using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace MediatR.CommandQuery.Queries
{
    public class EntitySelectQuery<TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
    {
        public EntitySelectQuery(IPrincipal principal, EntitySelect select)
            : base(principal)
        {
            Select = select;
        }

        public EntitySelect Select { get; }


        public override string GetCacheKey()
        {
            var hash = Select.GetHashCode();

            return $"{typeof(TReadModel).FullName}-Select-{hash}";
        }
    }
}