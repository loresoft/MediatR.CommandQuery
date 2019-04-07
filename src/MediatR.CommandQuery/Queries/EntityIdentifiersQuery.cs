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
            const int m = -1521134295;
            var hashCode = -346447222;

            foreach (var id in Ids)
                hashCode = hashCode * m + EqualityComparer<TKey>.Default.GetHashCode(id);

            return $"{typeof(TReadModel).FullName}-{hashCode}";
        }
    }
}
