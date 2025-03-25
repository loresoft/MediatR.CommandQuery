using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// A command to apply the <typeparamref name="TUpdateModel"/> to the entity with the specified identifier.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TUpdateModel">The type of the update model.</typeparam>
/// <typeparam name="TReadModel">The type of the read model.</typeparam>
public record EntityUpdateCommand<TKey, TUpdateModel, TReadModel>
    : EntityModelCommand<TUpdateModel, TReadModel>, ICacheExpire
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityUpdateCommand{TKey, TUpdateModel, TReadModel}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal" /> this command is run for</param>
    /// <param name="id">The identifier to apply the update to.</param>
    /// <param name="model">The update model to apply.</param>
    /// <exception cref="System.ArgumentNullException">When <paramref name="id"/> or <paramref name="model"/> is null</exception>
    public EntityUpdateCommand(ClaimsPrincipal? principal, [NotNull] TKey id, TUpdateModel model) : base(principal, model)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
    }

    /// <summary>
    /// Gets the identifier for entity to update.
    /// </summary>
    /// <value>
    /// The identifier for entity to update.
    /// </value>
    [NotNull]
    [JsonPropertyName("id")]
    public TKey Id { get; }

    /// <inheritdoc />
    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
