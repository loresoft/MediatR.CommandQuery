using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

using Microsoft.AspNetCore.JsonPatch;

namespace MediatR.CommandQuery.Commands;

public class EntityPatchCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>
{
    public EntityPatchCommand(IPrincipal? principal, [NotNull] TKey id, [NotNull] IJsonPatchDocument patch) : base(principal, id)
    {
        Patch = patch ?? throw new ArgumentNullException(nameof(patch));
    }

    public IJsonPatchDocument Patch { get; }


    public override string ToString()
    {
        return $"Entity Patch Command; Model: {typeof(TReadModel).Name}; {base.ToString()}";
    }

}
