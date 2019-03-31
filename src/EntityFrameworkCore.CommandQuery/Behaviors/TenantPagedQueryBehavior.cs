using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class TenantPagedQueryBehavior<TKey, TEntityModel>
        : TenantFilterBehaviorBase<TKey, TEntityModel, EntityPagedQuery<TEntityModel>, EntityPagedResult<TEntityModel>>
    {
        public TenantPagedQueryBehavior(ILoggerFactory loggerFactory, ITenantResolver<TKey> tenantResolver)
            : base(loggerFactory, tenantResolver)
        {
        }

        protected override async Task<EntityPagedResult<TEntityModel>> Process(
            EntityPagedQuery<TEntityModel> request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<EntityPagedResult<TEntityModel>> next)
        {
            // add tenant filter
            request.Query.Filter = RewriteFilter(request.Query.Filter, request.Principal);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}
