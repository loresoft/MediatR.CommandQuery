using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// A base command for commands that use an identifier
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract record EntityIdentifierCommand<TKey, TResponse>
    : PrincipalCommandBase<TResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityIdentifierCommand{TKey, TResponse}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal"/> this command is run for</param>
    /// <param name="id">The identifier for this command.</param>
    /// <exception cref="System.ArgumentNullException">When <paramref name="id"/> is null</exception>
    protected EntityIdentifierCommand(ClaimsPrincipal? principal, [NotNull] TKey id)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
    }

    /// <summary>
    /// Gets the identifier for this command.
    /// </summary>
    /// <value>
    /// The identifier for this command.
    /// </value>
    [NotNull]
    [JsonPropertyName("id")]
    public TKey Id { get; }
}
