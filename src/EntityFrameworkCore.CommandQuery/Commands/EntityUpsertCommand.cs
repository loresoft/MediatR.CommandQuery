using System;
using System.Security.Principal;

namespace EntityFrameworkCore.CommandQuery.Commands
{
    public class EntityUpsertCommand<TKey, TUpdateModel, TReadModel>
        : EntityModelCommand<TUpdateModel, TReadModel>
    {
        public EntityUpsertCommand(TKey id, TUpdateModel model, IPrincipal principal) : base(model, principal)
        {
            Id = id;
        }

        public TKey Id { get; }
    }
}