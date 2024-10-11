using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

public abstract record EntityModelCommand<TEntityModel, TReadModel>
    : PrincipalCommandBase<TReadModel>
{
    protected EntityModelCommand(ClaimsPrincipal? principal, [NotNull] TEntityModel model)
        : base(principal)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        Model = model;
    }

    [NotNull]
    public TEntityModel Model { get; }
}
