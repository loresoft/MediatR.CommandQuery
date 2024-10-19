using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Commands;

public record EntityUpsertCommand<TKey, TUpdateModel, TReadModel>
    : EntityModelCommand<TUpdateModel, TReadModel>, ICacheExpire
{
    public EntityUpsertCommand(ClaimsPrincipal? principal, [NotNull] TKey id, TUpdateModel model) : base(principal, model)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
    }

    [NotNull]
    public TKey Id { get; }

    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
