using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityListQuery<TReadModel> : IRequest<EntityListResult<TReadModel>>
    {
        public EntityListQuery(EntityQuery query, IPrincipal principal)
        {
            Query = query;
            Principal = principal;
        }

        public IPrincipal Principal { get; set; }

        public EntityQuery Query { get; set; }
    }
}