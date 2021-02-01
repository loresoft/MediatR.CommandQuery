using System;
using System.Security.Principal;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Commands
{
    public class EntityCreateCommand<TCreateModel, TReadModel>
        : EntityModelCommand<TCreateModel, TReadModel>
    {
        public EntityCreateCommand(IPrincipal principal, TCreateModel model) : base(principal, model)
        {
        }

        public override string ToString()
        {
            return $"Entity Create Command; Model: {typeof(TCreateModel).Name}; {base.ToString()}";
        }
    }
}
