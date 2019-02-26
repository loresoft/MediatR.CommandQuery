using System;
using System.Security.Principal;
using EntityFrameworkCore.CommandQuery.Definitions;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public class EntityUpdateCommand<TKey, TUpdateModel, TReadModel>
        : EntityModelCommand<TUpdateModel, TReadModel>
    {
        public EntityUpdateCommand(TKey id, TUpdateModel model, IPrincipal principal) : base(model, principal)
        {
            Id = id;
        }

        public TKey Id { get; }

        public TReadModel Original { get; set; }
    }
}