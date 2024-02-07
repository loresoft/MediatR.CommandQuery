using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

using SystemTextJsonPatch;

namespace MediatR.CommandQuery.Commands;

public class EntityPatchCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>
{
    public EntityPatchCommand(IPrincipal? principal, [NotNull] TKey id, [NotNull] JsonPatchDocument patch) : base(principal, id)
    {
        Patch = patch ?? throw new ArgumentNullException(nameof(patch));
    }

    public JsonPatchDocument Patch { get; }


    public override string ToString()
    {
        return $"Entity Patch Command; Model: {typeof(TReadModel).Name}; {base.ToString()}";
    }

}
