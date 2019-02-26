using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Definitions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class TrackUpdateCommandBehavior<TKey, TEntityModel, TResponse>
        : PipelineBehaviorBase<EntityUpdateCommand<TKey, TEntityModel, TResponse>, TResponse>
        where TEntityModel : class
    {
        public TrackUpdateCommandBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        protected override async Task<TResponse> Process(EntityUpdateCommand<TKey, TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var identityName = request.Principal?.Identity?.Name;
            var model = request.Model;

            if (model is ITrackUpdated updatedModel)
            {
                updatedModel.Updated = DateTimeOffset.UtcNow;
                updatedModel.UpdatedBy = identityName;
            }

            // continue pipeline
            return await next().ConfigureAwait(false);
        }
    }
}