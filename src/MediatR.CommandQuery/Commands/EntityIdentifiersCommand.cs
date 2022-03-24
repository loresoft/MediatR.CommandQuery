using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands;

public abstract class EntityIdentifiersCommand<TKey, TResponse>
    : PrincipalCommandBase<TResponse>
{
    protected EntityIdentifiersCommand(IPrincipal? principal, [NotNull] IEnumerable<TKey> ids)
        : base(principal)
    {
        if (ids is null)
            throw new ArgumentNullException(nameof(ids));

        Ids = ids.ToList();
    }

    public IReadOnlyCollection<TKey> Ids { get; }
}
