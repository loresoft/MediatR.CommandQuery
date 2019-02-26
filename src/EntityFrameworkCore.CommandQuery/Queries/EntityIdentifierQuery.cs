using System;
using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityIdentifierQuery<TKey, TReadModel> : IRequest<TReadModel>
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
