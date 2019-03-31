using System.Collections.Generic;
using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntitySelectQuery<TReadModel> : PrincipalQueryBase<IReadOnlyCollection<TReadModel>>
    {
        public EntitySelectQuery(IPrincipal principal)
            : this(null, (IEnumerable<EntitySort>)null, principal)
        {
        }

        public EntitySelectQuery(EntityFilter filter, IPrincipal principal)
            : this(filter, (IEnumerable<EntitySort>)null, principal)
        {
        }

        public EntitySelectQuery(EntityFilter filter, EntitySort sort, IPrincipal principal)
            : this(filter, new[] { sort }, principal)
        {
        }

        public EntitySelectQuery(EntityFilter filter, IEnumerable<EntitySort> sort, IPrincipal principal)
            : base(principal)
        {
            Filter = filter;
            Sort = sort;
        }

        public EntityFilter Filter { get; set; }

        public IEnumerable<EntitySort> Sort { get; set; }
    }
}