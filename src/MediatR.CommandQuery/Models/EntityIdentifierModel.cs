using System.Diagnostics.CodeAnalysis;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models;

public class EntityIdentifierModel<TKey> : IHaveIdentifier<TKey>
{
    [NotNull]
    public required TKey Id { get; set; } = default!;
}
