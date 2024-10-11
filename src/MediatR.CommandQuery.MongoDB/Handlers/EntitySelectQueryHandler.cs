using System.Linq.Dynamic.Core;

using AutoMapper;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public class EntitySelectQueryHandler<TRepository, TEntity, TKey, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, TKey, EntitySelectQuery<TReadModel>, IResult<IReadOnlyCollection<TReadModel>>>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TEntity : class
{
    public EntitySelectQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override Task<IResult<IReadOnlyCollection<TReadModel>>> Process(EntitySelectQuery<TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var query = Repository.All();

        // build query from filter
        query = BuildQuery(request, query);

        // page the query and convert to read model
        var result = QueryList(request, query, cancellationToken);

        //TODO make async?
        return Task.FromResult<IResult<IReadOnlyCollection<TReadModel>>>(Result.Ok(result));
    }


    protected virtual IQueryable<TEntity> BuildQuery(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query)
    {
        var entitySelect = request?.Select;

        // build query from filter
        if (entitySelect?.Filter != null)
            query = query.Filter(entitySelect.Filter);

        // add raw query
        if (!string.IsNullOrEmpty(entitySelect?.Query))
            query = query.Where(entitySelect.Query);

        return query;
    }

    protected virtual IReadOnlyCollection<TReadModel> QueryList(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        var results = query
            .Sort(request?.Select?.Sort)
            .ToList();

        return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
    }

}
