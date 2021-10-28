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
        private readonly IPrincipalReader _principalReader;

        public TrackChangeCommandBehavior(ILoggerFactory loggerFactory, IPrincipalReader principalReader) : base(loggerFactory)
        {
            _principalReader = principalReader;
        }

        protected override async Task<TResponse> Process(EntityModelCommand<TEntityModel, TResponse> request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TrackChange(request);

            // continue pipeline
            return await next().ConfigureAwait(false);
        }

        private void TrackChange(EntityModelCommand<TEntityModel, TResponse> request)
        {
            var identityName = _principalReader.GetIdentifier(request.Principal);
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
