using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public class EntityCreateCommandHandler<TRepository, TEntity, TKey, TCreateModel, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityCreateCommand<TCreateModel, TReadModel>, IResult<TReadModel>>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityCreateCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<IResult<TReadModel>> Process(EntityCreateCommand<TCreateModel, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        // create new entity from model
        var entity = Mapper.Map<TEntity>(request.Model);

        // apply create metadata
        if (entity is ITrackCreated createdModel)
        {
            createdModel.Created = request.Activated;
            createdModel.CreatedBy = request.ActivatedBy;
        }

        // apply update metadata
        if (entity is ITrackUpdated updateEntity)
        {
            updateEntity.Updated = request.Activated;
            updateEntity.UpdatedBy = request.ActivatedBy;
        }

        var result = await Repository
            .InsertAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        // convert to read model
        var model = Mapper.Map<TReadModel>(result);

        return Result.Ok(model);
    }
}
