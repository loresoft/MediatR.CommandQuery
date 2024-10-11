using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using SystemTextJsonPatch;

namespace MediatR.CommandQuery.Commands;

public record EntityPatchCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>
{
    public EntityPatchCommand(ClaimsPrincipal? principal, [NotNull] TKey id, [NotNull] JsonPatchDocument patch) : base(principal, id)
    {
        Patch = patch ?? throw new ArgumentNullException(nameof(patch));
    }

    public JsonPatchDocument Patch { get; }
}
