using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands;

public class EntityCreateCommand<TCreateModel, TReadModel>
    : EntityModelCommand<TCreateModel, TReadModel>
{
    public EntityCreateCommand(IPrincipal principal, [NotNull] TCreateModel model) : base(principal, model)
    {
    }

    public override string ToString()
    {
        return $"Entity Create Command; Model: {typeof(TCreateModel).Name}; {base.ToString()}";
    }
}
