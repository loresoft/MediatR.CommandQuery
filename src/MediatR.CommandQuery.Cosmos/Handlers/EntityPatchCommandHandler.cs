using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityPatchCommandHandler<TRepository, TEntity, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityPatchCommand<string, TReadModel>, TReadModel>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class, IHaveIdentifier<string>, new()
{
    public EntityPatchCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<TReadModel> Process(EntityPatchCommand<string, TReadModel> request, CancellationToken cancellationToken)
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

        // apply json patch to entity
        var jsonPatch = new JsonPatchDocument(
            request.Patch.GetOperations().ToList(),
            request.Patch.ContractResolver);

        jsonPatch.ApplyTo(entity);

        // apply update metadata
        if (entity is ITrackUpdated updateEntity)
        {
            updateEntity.Updated = request.Activated;
            updateEntity.UpdatedBy = request.ActivatedBy;
        }

        var savedEntity = await Repository
            .UpdateAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        // return read model
        var model = Mapper.Map<TReadModel>(savedEntity);
        return model;
    }
}
