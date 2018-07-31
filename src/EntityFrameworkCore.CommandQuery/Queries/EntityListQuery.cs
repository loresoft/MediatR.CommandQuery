using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    // ReSharper disable once UnusedTypeParameter
    public class EntityListQuery<TEntity, TReadModel> : IRequest<EntityListResult<TReadModel>>
        where TEntity : class
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