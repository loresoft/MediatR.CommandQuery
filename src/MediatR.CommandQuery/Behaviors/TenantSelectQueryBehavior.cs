using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class TenantSelectQueryBehavior<TKey, TEntityModel>
    : TenantFilterBehaviorBase<TKey, TEntityModel, EntitySelectQuery<TEntityModel>, IReadOnlyCollection<TEntityModel>>
    where TEntityModel : class
{
    public TenantSelectQueryBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver)
        : base(loggerFactory, tenantResolver)
    {
    }

    protected override async Task<IReadOnlyCollection<TEntityModel>> Process(
        EntitySelectQuery<TEntityModel> request,
        RequestHandlerDelegate<IReadOnlyCollection<TEntityModel>> next,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (next is null)
            throw new ArgumentNullException(nameof(next));

        // add tenant filter
        request.Select.Filter = await RewriteFilter(request.Select?.Filter, request.Principal).ConfigureAwait(false);

        // continue pipeline
        return await next().ConfigureAwait(false);
    }
}
