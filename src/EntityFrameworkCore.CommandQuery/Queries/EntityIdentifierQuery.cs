using System;
using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    // ReSharper disable once UnusedTypeParameter
    public class EntityIdentifierQuery<TKey, TEntity, TReadModel> : IRequest<TReadModel>
        where TEntity : class, new()
    {
        public EntityIdentifierQuery(TKey id, IPrincipal principal)
        {
            Principal = principal;
            Id = id;
        }

        public IPrincipal Principal { get; set; }

        public TKey Id { get; set; }
    }
}
