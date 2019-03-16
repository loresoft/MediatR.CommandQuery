using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityIdentifiersQuery<TKey, TReadModel> : PrincipalQueryBase<IReadOnlyCollection<TReadModel>>
    {
        public EntityIdentifiersQuery(IEnumerable<TKey> ids, IPrincipal principal)
            : base(principal)
        {
            Ids = ids.ToList();
        }

        public IReadOnlyCollection<TKey> Ids { get; set; }
    }
}
