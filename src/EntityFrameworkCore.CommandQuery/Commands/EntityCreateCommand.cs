using System;
using System.Security.Principal;
using EntityFrameworkCore.CommandQuery.Definitions;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public class EntityCreateCommand<TCreateModel, TReadModel>
        : EntityModelCommand<TCreateModel, TReadModel>
    {
        public EntityCreateCommand(TCreateModel model, IPrincipal principal) : base(model, principal)
        {
        }
    }
}
