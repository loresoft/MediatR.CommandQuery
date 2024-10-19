using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Commands;

public abstract record EntityIdentifierCommand<TKey, TResponse>
    : PrincipalCommandBase<TResponse>
{
    protected EntityIdentifierCommand(ClaimsPrincipal? principal, [NotNull] TKey id)
        : base(principal)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }
}
