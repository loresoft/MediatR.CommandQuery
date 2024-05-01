using AutoMapper;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public class EntityIdentifiersQueryHandler<TRepository, TEntity, TKey, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityIdentifiersQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper) : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntityIdentifiersQuery<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var keys = new HashSet<TKey>(request.Ids);

        var results = await Repository.Collection
            .AsQueryable()
            .Where(p => keys.Contains(p.Id))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
    }
}
