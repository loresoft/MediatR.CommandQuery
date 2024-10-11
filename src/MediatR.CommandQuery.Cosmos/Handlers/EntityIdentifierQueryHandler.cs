using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityIdentifierQueryHandler<TRepository, TEntity, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityIdentifierQuery<string, TReadModel>, IResult<TReadModel>>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class, IHaveIdentifier<string>, new()
{
    public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper) : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<IResult<TReadModel>> Process(EntityIdentifierQuery<string, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (!CosmosKey.TryDecode(request.Id, out var id, out var partitionKey))
            throw new InvalidOperationException("Invalid Cosmos Key format");

        var entity = await Repository
            .FindAsync(id, partitionKey, cancellationToken)
            .ConfigureAwait(false);

        // convert deleted entity to read model
        var model = Mapper.Map<TReadModel>(entity);

        return Result.Ok(model);
    }
}
