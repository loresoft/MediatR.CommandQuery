using System;
using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public abstract class EntityModelCommand<TEntityModel, TReadModel>
        : PrincipalCommandBase<TReadModel>
    {
        protected EntityModelCommand(TEntityModel model, IPrincipal principal)
            : base(principal)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Model = model;
        }

        public TEntityModel Model { get; }
    }
}