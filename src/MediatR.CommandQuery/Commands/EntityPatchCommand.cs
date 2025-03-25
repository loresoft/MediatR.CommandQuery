using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

using SystemTextJsonPatch;

namespace MediatR.CommandQuery.Commands;

/// <summary>
/// A command to apply a JSON patch to the entity with the specified identifier.
/// <typeparamref name="TReadModel"/> will be the result of the command.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TReadModel">The type of the read model.</typeparam>
public record EntityPatchCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>, ICacheExpire
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityPatchCommand{TKey, TReadModel}"/> class.
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal" /> this command is run for</param>
    /// <param name="id">The identifier to apply JSON patch to.</param>
    /// <param name="patch">The JSON patch to apply.</param>
    /// <exception cref="System.ArgumentNullException">When <paramref name="id"/> or <paramref name="patch"/> is null</exception>
    public EntityPatchCommand(ClaimsPrincipal? principal, [NotNull] TKey id, [NotNull] JsonPatchDocument patch)
        : base(principal, id)
    {
        ArgumentNullException.ThrowIfNull(patch);

        Patch = patch;
    }

    /// <summary>
    /// Gets the JSON patch to apply to the entity with the specified identifier.
    /// </summary>
    /// <value>
    /// The patch.
    /// </value>
    [JsonPropertyName("patch")]
    public JsonPatchDocument Patch { get; }

    /// <inheritdoc />
    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
