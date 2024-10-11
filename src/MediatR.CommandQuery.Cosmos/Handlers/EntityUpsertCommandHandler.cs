using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityUpsertCommandHandler<TRepository, TEntity, TUpdateModel, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityUpsertCommand<string, TUpdateModel, TReadModel>, IResult<TReadModel>>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class, IHaveIdentifier<string>, new()
{
    public EntityUpsertCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<IResult<TReadModel>> Process(EntityUpsertCommand<string, TUpdateModel, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (!CosmosKey.TryDecode(request.Id, out var id, out var partitionKey))
            throw new InvalidOperationException("Invalid Cosmos Key format");

        var entity = await Repository
            .FindAsync(id, partitionKey, cancellationToken)
            .ConfigureAwait(false);

        // create entity if not found
        if (entity == null)
        {
            entity = new TEntity { Id = id };

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
            .UpdateAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        // return read model
        var model = Mapper.Map<TReadModel>(savedEntity);

        return Result.Ok(model);
    }
}
