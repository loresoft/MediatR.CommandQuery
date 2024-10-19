using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Commands;

public record EntityDeleteCommand<TKey, TReadModel>
    : EntityIdentifierCommand<TKey, TReadModel>, ICacheExpire
{
    public EntityDeleteCommand(ClaimsPrincipal? principal, [NotNull] TKey id) : base(principal, id)
    {
    }

    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
