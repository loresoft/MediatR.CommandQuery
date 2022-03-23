using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public class EntityIdentifierQueryHandler<TRepository, TEntity, TKey, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityIdentifierQuery<TKey, TReadModel>, TReadModel>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper) : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<TReadModel> Process(EntityIdentifierQuery<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var entity = await Repository
            .FindAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        // convert entity to read model
        var model = Mapper.Map<TReadModel>(entity);

        return model;
    }
}
