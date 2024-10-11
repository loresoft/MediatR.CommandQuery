using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MediatR.CommandQuery.Commands;

public record EntityDeleteCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>
{
    public EntityDeleteCommand(ClaimsPrincipal? principal, [NotNull] TKey id) : base(principal, id)
    {
    }
}
