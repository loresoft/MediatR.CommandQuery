using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Queries;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class DeletedSelectQueryBehavior<TEntityModel>
        : DeletedFilterBehaviorBase<TEntityModel, EntitySelectQuery<TEntityModel>, IReadOnlyCollection<TEntityModel>>
    {
        public DeletedSelectQueryBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        protected override async Task<IReadOnlyCollection<TEntityModel>> Process(EntitySelectQuery<TEntityModel> request, CancellationToken cancellationToken, RequestHandlerDelegate<IReadOnlyCollection<TEntityModel>> next)
        {
            // add delete filter
            request.Select.Filter = RewriteFilter(request.Select.Filter, request.Principal);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}