using System;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public abstract class EntityModelCommand<TEntityModel, TReadModel>
        : PrincipalCommandBase<TReadModel>
    {
        protected EntityModelCommand(IPrincipal principal, TEntityModel model)
            : base(principal)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Model = model;
        }

        public TEntityModel Model { get; }
    }
}