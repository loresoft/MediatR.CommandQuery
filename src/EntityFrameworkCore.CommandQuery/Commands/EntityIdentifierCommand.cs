using System;
using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TReadModel>
        : IRequest<TReadModel>
    {
        protected EntityIdentifierCommand(TKey id, IPrincipal principal)
        {
            Id = id;
            Principal = principal;
        }

        public IPrincipal Principal { get; }

        public TKey Id { get; }
    }
}