using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// Base class for using a view model for the command
/// </summary>
/// <typeparam name="TEntityModel">The type of the model for this command.</typeparam>
/// <typeparam name="TReadModel">The type of the read model to return.</typeparam>
/// <seealso cref="MediatR.CommandQuery.Commands.PrincipalCommandBase&lt;TReadModel&gt;" />
/// <seealso cref="MediatR.IRequest&lt;TReadModel&gt;" />
/// <seealso cref="MediatR.IBaseRequest" />
public abstract record EntityModelCommand<TEntityModel, TReadModel>
    : PrincipalCommandBase<TReadModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityModelCommand{TEntityModel, TReadModel}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal"/> this command is run for</param>
    /// <param name="model">The model to use for this command.</param>
    protected EntityModelCommand(ClaimsPrincipal? principal, [NotNull] TEntityModel model)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(model);

        Model = model;
    }

    /// <summary>
    /// Gets the view model to use for this command.
    /// </summary>
    /// <value>
    /// The view model to use for this command.
    /// </value>
    [NotNull]
    public TEntityModel Model { get; }
}
