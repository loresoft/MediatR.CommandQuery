using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class TenantSelectQueryBehavior<TKey, TEntityModel>
        : TenantFilterBehaviorBase<TKey, TEntityModel, EntitySelectQuery<TEntityModel>, IReadOnlyCollection<TEntityModel>>
    {
        public TenantSelectQueryBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver)
            : base(loggerFactory, tenantResolver)
        {
        }

        protected override async Task<IReadOnlyCollection<TEntityModel>> Process(
            EntitySelectQuery<TEntityModel> request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<IReadOnlyCollection<TEntityModel>> next)
        {
            // add tenant filter
            request.Filter = await RewriteFilter(request.Filter, request.Principal).ConfigureAwait(false);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}