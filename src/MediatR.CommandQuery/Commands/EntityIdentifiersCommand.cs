using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

public abstract record EntityIdentifiersCommand<TKey, TResponse>
    : PrincipalCommandBase<TResponse>
{
    protected EntityIdentifiersCommand(ClaimsPrincipal? principal, [NotNull] IEnumerable<TKey> ids)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(ids);

        Ids = ids.ToList();
    }

    public IReadOnlyCollection<TKey> Ids { get; }
}
