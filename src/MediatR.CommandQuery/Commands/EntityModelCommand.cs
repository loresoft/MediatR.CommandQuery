using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands;

public abstract class EntityModelCommand<TEntityModel, TReadModel>
    : PrincipalCommandBase<TReadModel>
{
    protected EntityModelCommand(IPrincipal? principal, [NotNull] TEntityModel model)
        : base(principal)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        Model = model;
    }

    [NotNull]
    public TEntityModel Model { get; }
}
