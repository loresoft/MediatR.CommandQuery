using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Definitions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
{
    public class TrackCreateCommandBehavior<TEntityModel, TResponse>
        : PipelineBehaviorBase<EntityCreateCommand<TEntityModel, TResponse>, TResponse>
        where TEntityModel : class
    {
        public TrackCreateCommandBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        protected override async Task<TResponse> Process(EntityCreateCommand<TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var identityName = request.Principal?.Identity?.Name;
            var model = request.Model;

            if (model is ITrackCreated createdModel)
            {
                createdModel.Created = DateTimeOffset.UtcNow;
                createdModel.CreatedBy = identityName;
            }

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
