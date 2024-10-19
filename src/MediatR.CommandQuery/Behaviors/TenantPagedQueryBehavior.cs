using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class TenantPagedQueryBehavior<TKey, TEntityModel>
    : TenantFilterBehaviorBase<TKey, TEntityModel, EntityPagedQuery<TEntityModel>, EntityPagedResult<TEntityModel>>
    where TEntityModel : class
{
    public TenantPagedQueryBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver)
        : base(loggerFactory, tenantResolver)
    {
    }

    protected override async Task<EntityPagedResult<TEntityModel>> Process(
        EntityPagedQuery<TEntityModel> request,
        RequestHandlerDelegate<EntityPagedResult<TEntityModel>> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        // add tenant filter
        request.Query.Filter = await RewriteFilter(request.Query?.Filter, request.Principal).ConfigureAwait(false);

        // continue pipeline
        return await next().ConfigureAwait(false);
    }
}
