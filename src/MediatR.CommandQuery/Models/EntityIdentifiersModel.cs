using System.Diagnostics.CodeAnalysis;

namespace MediatR.CommandQuery.Models;

public class EntityIdentifiersModel<TKey>
{
    [NotNull]
    public required IReadOnlyCollection<TKey> Ids { get; set; } = null!;
}
