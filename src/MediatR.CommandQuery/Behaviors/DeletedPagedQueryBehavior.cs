using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class DeletedPagedQueryBehavior<TEntityModel>
    : DeletedFilterBehaviorBase<TEntityModel, EntityPagedQuery<TEntityModel>, EntityPagedResult<TEntityModel>>
    where TEntityModel : class
{
    public DeletedPagedQueryBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    protected override async Task<EntityPagedResult<TEntityModel>> Process(
        EntityPagedQuery<TEntityModel> request,
        RequestHandlerDelegate<EntityPagedResult<TEntityModel>> next,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (next is null)
            throw new ArgumentNullException(nameof(next));

        // add delete filter
        request.Query.Filter = RewriteFilter(request.Query?.Filter, request.Principal);

        // continue pipeline
        return await next().ConfigureAwait(false);
    }
}
