using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

using SystemTextJsonPatch;

namespace MediatR.CommandQuery.Commands;

public record EntityPatchCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>, ICacheExpire
{
    public EntityPatchCommand(ClaimsPrincipal? principal, [NotNull] TKey id, [NotNull] JsonPatchDocument patch) : base(principal, id)
    {
        ArgumentNullException.ThrowIfNull(patch);

        Patch = patch;
    }

    public JsonPatchDocument Patch { get; }

    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
