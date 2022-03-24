using System;
using System.Collections.Generic;
using System.Security.Principal;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors;

public abstract class DeletedFilterBehaviorBase<TEntityModel, TRequest, TResponse>
    : PipelineBehaviorBase<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly Lazy<bool> _supportsDelete = new(SupportsDelete);

    protected DeletedFilterBehaviorBase(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    protected virtual EntityFilter? RewriteFilter(EntityFilter? originalFilter, IPrincipal? principal)
    {
        if (!_supportsDelete.Value)
            return originalFilter;

        // don't rewrite if already has filter
        if (HasDeletedFilter(originalFilter))
            return originalFilter;

        var deletedFilter = new EntityFilter
        {
            Name = nameof(ITrackDeleted.IsDeleted),
            Value = false,
            Operator = EntityFilterOperators.Equal
        };

        if (originalFilter == null)
            return deletedFilter;

        var boolFilter = new EntityFilter
        {
            Logic = EntityFilterLogic.And,
            Filters = new List<EntityFilter>()
                {
                    deletedFilter,
                    originalFilter
                }
        };

        return boolFilter;
    }

    protected virtual bool HasDeletedFilter(EntityFilter? originalFilter)
    {
        if (originalFilter == null)
            return false;

        var stack = new Stack<EntityFilter>();
        stack.Push(originalFilter);

        while (stack.Count > 0)
        {
            var filter = stack.Pop();
            if (!string.IsNullOrEmpty(filter.Name) && filter.Name == nameof(ITrackDeleted.IsDeleted))
                return true;

            if (filter.Filters == null)
                continue;

            foreach (var innerFilter in filter.Filters)
                stack.Push(innerFilter);
        }

        return false;
    }

    private static bool SupportsDelete()
    {
        var interfaceType = typeof(ITrackDeleted);
        var entityType = typeof(TEntityModel);

        return interfaceType.IsAssignableFrom(entityType);
    }
}
