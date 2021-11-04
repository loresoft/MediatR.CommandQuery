using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace MediatR.CommandQuery.Queries
{
    public class EntityIdentifiersQuery<TKey, TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
    {
        public EntityIdentifiersQuery(IPrincipal principal, IEnumerable<TKey> ids)
            : base(principal)
        {
            Ids = ids.ToList();
        }

        public IReadOnlyCollection<TKey> Ids { get; }

        public override string GetCacheKey()
        {
            var hash = new HashCode();

            foreach (var id in Ids)
                hash.Add(id);

            return $"{typeof(TReadModel).FullName}-{hash.ToHashCode()}";
        }
    }
}
