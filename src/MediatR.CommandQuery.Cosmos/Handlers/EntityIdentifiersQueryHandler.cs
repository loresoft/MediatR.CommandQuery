using AutoMapper;

using Cosmos.Abstracts;
using Cosmos.Abstracts.Extensions;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityIdentifiersQueryHandler<TRepository, TEntity, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityIdentifiersQuery<string, TReadModel>, IReadOnlyCollection<TReadModel>>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class, IHaveIdentifier<string>, new()
{
    public EntityIdentifiersQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper) : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntityIdentifiersQuery<string, TReadModel> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var keys = new HashSet<string>(StringComparer.Ordinal);
        foreach (var requestId in request.Ids)
        {
            if (CosmosKey.TryDecode(requestId, out var id, out PartitionKey partitionKey))
                keys.Add(id);
        }

        var query = await Repository
            .GetQueryableAsync()
            .ConfigureAwait(false);

        var results = await query
            .Where(p => keys.Contains(p.Id))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
    }
}
