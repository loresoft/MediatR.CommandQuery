using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers
{
    public class EntityUpdateCommandHandler<TRepository, TEntity, TKey, TUpdateModel, TReadModel>
        : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>
        where TRepository : IMongoRepository<TEntity, TKey>
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityUpdateCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
            : base(loggerFactory, repository, mapper)
        {
        }

        protected override async Task<TReadModel> Process(EntityUpdateCommand<TKey, TUpdateModel, TReadModel> request, CancellationToken cancellationToken)
        {
            var entity = await Repository
                .FindAsync(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default;

            // copy updates from model to entity
            Mapper.Map(request.Model, entity);

            // apply update metadata
            if (entity is ITrackUpdated updateEntity)
            {
                updateEntity.Updated = request.Activated;
                updateEntity.UpdatedBy = request.ActivatedBy;
            }

            // save updates
            var savedEntity = await Repository
                .UpdateAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var model = Mapper.Map<TReadModel>(savedEntity);
            return model;
        }
    }
}
