using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// A base command for commands that use a list of identifiers
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract record EntityIdentifiersCommand<TKey, TResponse>
    : PrincipalCommandBase<TResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityIdentifiersCommand{TKey, TResponse}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal"/> this command is run for</param>
    /// <param name="ids">The list of identifiers for this command.</param>
    /// <exception cref="System.ArgumentNullException">When <paramref name="ids"/> is null</exception>
    protected EntityIdentifiersCommand(ClaimsPrincipal? principal, [NotNull] IReadOnlyCollection<TKey> ids)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(ids);

        Ids = ids;
    }

    /// <summary>
    /// Gets the list of identifiers for this command.
    /// </summary>
    /// <value>
    /// The list of identifiers for this command.
    /// </value>
    public IReadOnlyCollection<TKey> Ids { get; }
}
