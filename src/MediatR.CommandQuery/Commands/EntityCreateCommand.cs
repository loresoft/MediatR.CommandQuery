using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Services;

namespace MediatR.CommandQuery.Commands;

public record EntityCreateCommand<TCreateModel, TReadModel>
    : EntityModelCommand<TCreateModel, TReadModel>, ICacheExpire
{
    public EntityCreateCommand(ClaimsPrincipal? principal, [NotNull] TCreateModel model)
        : base(principal, model)
    {
    }

    string? ICacheExpire.GetCacheTag()
        => CacheTagger.GetTag<TReadModel>();
}
