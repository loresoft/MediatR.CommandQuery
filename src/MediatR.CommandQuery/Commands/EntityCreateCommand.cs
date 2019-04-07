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
    }
}
