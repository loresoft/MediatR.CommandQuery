using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers
{
    public class EntityDeleteCommandHandler<TRepository, TEntity, TKey, TReadModel>
        : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityDeleteCommand<TKey, TReadModel>, TReadModel>
        where TRepository : IMongoRepository<TEntity, TKey>
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {

        public EntityDeleteCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
            : base(loggerFactory, repository, mapper)
        {
        }

        protected override async Task<TReadModel> Process(EntityDeleteCommand<TKey, TReadModel> request, CancellationToken cancellationToken)
        {
            var entity = await Repository
                .FindAsync(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default;

            // apply update metadata
            if (entity is ITrackUpdated updateEntity)
            {
                updateEntity.UpdatedBy = request.ActivatedBy;
                updateEntity.Updated = request.Activated;
            }

            TEntity savedEntity;

            // entity supports soft delete
            if (entity is ITrackDeleted deleteEntity)
            {
                deleteEntity.IsDeleted = true;

                savedEntity = await Repository
                    .UpdateAsync(entity, cancellationToken)
                    .ConfigureAwait(false);
            }
            else
            {
                // when history is tracked, need to update the entity with update metadata before deleting
                if (entity is ITrackHistory && entity is ITrackUpdated)
                    savedEntity = await Repository
                        .UpdateAsync(entity, cancellationToken)
                        .ConfigureAwait(false);
                else
                    savedEntity = entity;

                await Repository
                    .DeleteAsync(entity, cancellationToken)
                    .ConfigureAwait(false);
            }

            // convert deleted entity to read model
            var model = Mapper.Map<TReadModel>(savedEntity);
            return model;
        }

    }
}
