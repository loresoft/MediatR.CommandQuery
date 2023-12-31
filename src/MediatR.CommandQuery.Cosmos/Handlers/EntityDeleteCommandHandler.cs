using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityDeleteCommandHandler<TRepository, TEntity, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityDeleteCommand<string, TReadModel>, TReadModel>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class, IHaveIdentifier<string>, new()
{

    public EntityDeleteCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<TReadModel> Process(EntityDeleteCommand<string, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (!CosmosKey.TryDecode(request.Id, out var id, out var partitionKey))
            throw new InvalidOperationException("Invalid Cosmos Key format");

        var entity = await Repository
            .FindAsync(id, partitionKey, cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
            return default!;

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
            if (entity is ITrackHistory and ITrackUpdated)
            {
                savedEntity = await Repository
                   .UpdateAsync(entity, cancellationToken)
                   .ConfigureAwait(false);
            }
            else
            {
                savedEntity = entity;
            }

            await Repository
                .DeleteAsync(entity, cancellationToken)
                .ConfigureAwait(false);
        }

        // convert deleted entity to read model
        var model = Mapper.Map<TReadModel>(savedEntity);
        return model;
    }

}
