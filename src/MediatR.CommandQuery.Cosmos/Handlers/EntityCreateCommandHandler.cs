using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityCreateCommandHandler<TRepository, TEntity, TCreateModel, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityCreateCommand<TCreateModel, TReadModel>, IResult<TReadModel>>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class, IHaveIdentifier<string>, new()
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
            .CreateAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        // convert to read model
        var model = Mapper.Map<TReadModel>(result);

        return Result.Ok(model);
    }
}
