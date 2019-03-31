using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityPagedQuery<TReadModel> : PrincipalQueryBase<EntityPagedResult<TReadModel>>
    {
        public EntityPagedQuery(EntityQuery query, IPrincipal principal)
            : base(principal)
        {
            Query = query;
        }

        public EntityQuery Query { get; set; }
    }
}