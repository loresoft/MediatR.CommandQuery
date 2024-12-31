using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// A command to delete based on the specified identifier.
/// <typeparamref name="TReadModel"/> will be the result of the command.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TReadModel">The type of the read model.</typeparam>
public record EntityDeleteCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>, ICacheExpire
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDeleteCommand{TKey, TReadModel}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal" /> this command is run for</param>
    /// <param name="id">The identifier for this command.</param>
    public EntityDeleteCommand(ClaimsPrincipal? principal, [NotNull] TKey id) : base(principal, id)
    {
    }

    /// <inheritdoc />
    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
