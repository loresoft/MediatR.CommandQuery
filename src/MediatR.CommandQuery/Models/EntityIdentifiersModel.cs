using System.Collections.Generic;

namespace MediatR.CommandQuery.Models;

public class EntityIdentifiersModel<TKey>
{
    public IReadOnlyCollection<TKey> Ids { get; set; } = null!;
}
