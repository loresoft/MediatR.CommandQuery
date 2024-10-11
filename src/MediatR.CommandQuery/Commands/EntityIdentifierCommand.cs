using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

public abstract record EntityIdentifierCommand<TKey, TResponse>
    : PrincipalCommandBase<TResponse>
{
    protected EntityIdentifierCommand(ClaimsPrincipal? principal, [NotNull] TKey id)
        : base(principal)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }

    public override string ToString()
    {
        return $"Id: {Id}; {base.ToString()}";
    }

}
