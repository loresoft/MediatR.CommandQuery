using System.Collections.Generic;
using System.Security.Principal;

namespace MediatR.CommandQuery.Queries
{
    public class EntitySelectQuery<TReadModel> : CacheableQueryBase<IReadOnlyCollection<TReadModel>>
    {
        public EntitySelectQuery(IPrincipal principal)
            : this(principal, null, (IEnumerable<EntitySort>)null)
        {
        }

        public EntitySelectQuery(IPrincipal principal, EntityFilter filter)
            : this(principal, filter, (IEnumerable<EntitySort>)null)
        {
        }

        public EntitySelectQuery(IPrincipal principal, EntityFilter filter, EntitySort sort)
            : this(principal, filter, new[] { sort })
        {
        }

        public EntitySelectQuery(IPrincipal principal, EntityFilter filter, IEnumerable<EntitySort> sort)
            : base(principal)
        {
            Filter = filter;
            Sort = sort;
        }

        public EntityFilter Filter { get; set; }

        public IEnumerable<EntitySort> Sort { get; set; }


        public override string GetCacheKey()
        {
            const int m = -1521134295;
            var hashCode = -1241527264;

            if (Filter != null)
                hashCode = hashCode * m + Filter.GetHashCode();

            if (Sort != null)
                foreach (var s in Sort)
                    hashCode = hashCode * m + s.GetHashCode();

            return $"{typeof(TReadModel).FullName}-Select-{hashCode}";
        }
    }
}