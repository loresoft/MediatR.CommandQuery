using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands;

public class EntityUpdateCommand<TKey, TUpdateModel, TReadModel>
    : EntityModelCommand<TUpdateModel, TReadModel>
{
    public EntityUpdateCommand(IPrincipal principal, [NotNull] TKey id, TUpdateModel model) : base(principal, model)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }

    public override string ToString()
    {
        return $"Entity Update Command; Model: {typeof(TUpdateModel).Name}; Id: {Id}; {base.ToString()}";
    }
}
