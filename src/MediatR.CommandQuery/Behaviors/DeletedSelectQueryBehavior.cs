using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

/// <summary>
/// A behavior for appending soft delete (IsDeleted) filter
/// </summary>
/// <typeparam name="TEntityModel">The type of the entity model.</typeparam>
public class DeletedSelectQueryBehavior<TEntityModel>
    : DeletedFilterBehaviorBase<TEntityModel, EntitySelectQuery<TEntityModel>, IReadOnlyCollection<TEntityModel>>
    where TEntityModel : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletedSelectQueryBehavior{TEntityModel}"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    public DeletedSelectQueryBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    /// <inheritdoc />
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
