using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Definitions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Behaviors
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
                if (createdModel.Created == default(DateTimeOffset))
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
