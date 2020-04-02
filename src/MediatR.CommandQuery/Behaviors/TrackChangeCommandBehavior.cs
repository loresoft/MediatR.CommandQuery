using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Behaviors
{
    public class TrackChangeCommandBehavior<TEntityModel, TResponse>
        : PipelineBehaviorBase<EntityModelCommand<TEntityModel, TResponse>, TResponse>
        where TEntityModel : class
    {
        public TrackChangeCommandBehavior(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        protected override async Task<TResponse> Process(EntityModelCommand<TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TrackChange(request);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }

        private void TrackChange(EntityModelCommand<TEntityModel, TResponse> request)
        {
            var identityName = request.Principal?.Identity?.Name;
            var model = request.Model;

            if (model is ITrackCreated createdModel)
            {
                if (createdModel.Created == default)
                    createdModel.Created = DateTimeOffset.UtcNow;

                if (string.IsNullOrEmpty(createdModel.CreatedBy))
                    createdModel.CreatedBy = identityName;
            }

            if (model is ITrackUpdated updatedModel)
            {
                updatedModel.Updated = DateTimeOffset.UtcNow;
                updatedModel.UpdatedBy = identityName;
            }
        }
    }
}
