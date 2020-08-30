using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Queries;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class DeletedPagedQueryBehavior<TEntityModel>
        : DeletedFilterBehaviorBase<TEntityModel, EntityPagedQuery<TEntityModel>, EntityPagedResult<TEntityModel>>

    {
        public DeletedPagedQueryBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        protected override async Task<EntityPagedResult<TEntityModel>> Process(EntityPagedQuery<TEntityModel> request, CancellationToken cancellationToken, RequestHandlerDelegate<EntityPagedResult<TEntityModel>> next)
        {
            // add delete filter
            request.Query.Filter = RewriteFilter(request.Query.Filter, request.Principal);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}