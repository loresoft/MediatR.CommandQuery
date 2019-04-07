using System;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands
{
    public class EntityUpsertCommand<TKey, TUpdateModel, TReadModel>
        : EntityModelCommand<TUpdateModel, TReadModel>
    {
        public EntityUpsertCommand(IPrincipal principal, TKey id, TUpdateModel model) : base(principal, model)
        {
            Id = id;
        }

        public TKey Id { get; }
    }
}