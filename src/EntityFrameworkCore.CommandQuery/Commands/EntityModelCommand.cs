using System;
using System.Security.Principal;
using MediatR;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public abstract class EntityModelCommand<TEntityModel, TReadModel>
        : IRequest<TReadModel>
    {
        protected EntityModelCommand(TEntityModel model, IPrincipal principal)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Model = model;
            Principal = principal;
        }

        public IPrincipal Principal { get; }

        public TEntityModel Model { get; }

    }
}