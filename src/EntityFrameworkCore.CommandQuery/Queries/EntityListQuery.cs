using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityListQuery<TReadModel> : PrincipalQueryBase<EntityListResult<TReadModel>>
    {
        public EntityListQuery(EntityQuery query, IPrincipal principal)
            : base(principal)
        {
            Query = query;
        }

        public EntityQuery Query { get; set; }
    }
}