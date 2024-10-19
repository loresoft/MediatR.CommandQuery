using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public class DeletedSelectQueryBehavior<TEntityModel>
    : DeletedFilterBehaviorBase<TEntityModel, EntitySelectQuery<TEntityModel>, IReadOnlyCollection<TEntityModel>>
    where TEntityModel : class
{
    public DeletedSelectQueryBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    protected override async Task<IReadOnlyCollection<TEntityModel>> Process(
        EntitySelectQuery<TEntityModel> request,
        RequestHandlerDelegate<IReadOnlyCollection<TEntityModel>> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        // add delete filter
        request.Select.Filter = RewriteFilter(request.Select?.Filter, request.Principal);

        // continue pipeline
        return await next().ConfigureAwait(false);
    }
}
