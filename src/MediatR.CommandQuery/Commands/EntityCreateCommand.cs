using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

public record EntityCreateCommand<TCreateModel, TReadModel>
    : EntityModelCommand<TCreateModel, TReadModel>
{
    public EntityCreateCommand(ClaimsPrincipal? principal, [NotNull] TCreateModel model) : base(principal, model)
    {
    }

    public override string ToString()
    {
        return $"Entity Create Command; Model: {typeof(TCreateModel).Name}; {base.ToString()}";
    }
}
