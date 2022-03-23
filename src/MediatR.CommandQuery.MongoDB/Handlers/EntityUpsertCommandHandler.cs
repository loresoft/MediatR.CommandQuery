using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public class EntityUpsertCommandHandler<TRepository, TEntity, TKey, TUpdateModel, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityUpsertCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<TReadModel> Process(EntityUpsertCommand<TKey, TUpdateModel, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var entity = await Repository
            .FindAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        // create entity if not found
        if (entity == null)
        {
            entity = new TEntity { Id = request.Id };

            // apply create metadata
            if (entity is ITrackCreated createdModel)
            {
                createdModel.Created = request.Activated;
                createdModel.CreatedBy = request.ActivatedBy;
            }
        }

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
            .UpsertAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        // return read model
        var model = Mapper.Map<TReadModel>(savedEntity);
        return model;
    }
}
