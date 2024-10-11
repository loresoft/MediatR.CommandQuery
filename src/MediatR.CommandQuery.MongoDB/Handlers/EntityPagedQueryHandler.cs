using System.Linq.Dynamic.Core;

using AutoMapper;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public class EntityPagedQueryHandler<TRepository, TEntity, TKey, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityPagedQuery<TReadModel>, IResult<EntityPagedResult<TReadModel>>>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TEntity : class
{
    public EntityPagedQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<IResult<EntityPagedResult<TReadModel>>> Process(EntityPagedQuery<TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var query = Repository.All();

        // build query from filter
        query = BuildQuery(request, query);

        // get total for query
        int total = await QueryTotal(request, query, cancellationToken).ConfigureAwait(false);

        // short circuit if total is zero
        if (total == 0)
            return Result.Ok(new EntityPagedResult<TReadModel> { Data = [] });

        // page the query and convert to read model
        var result = await QueryPaged(request, query, cancellationToken);

        var pagedResult = new EntityPagedResult<TReadModel>
        {
            Total = total,
            Data = result
        };

        return Result.Ok(pagedResult);
    }


    protected virtual IQueryable<TEntity> BuildQuery(EntityPagedQuery<TReadModel> request, IQueryable<TEntity> query)
    {
        var entityQuery = request.Query;

        // build query from filter
        if (entityQuery?.Filter != null)
            query = query.Filter(entityQuery.Filter);

        // add raw query
        if (!string.IsNullOrEmpty(entityQuery?.Query))
            query = query.Where(entityQuery.Query);

        return query;
    }

    protected virtual async Task<int> QueryTotal(EntityPagedQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        var mongoQuery = query as IMongoQueryable<TEntity>;

        return mongoQuery != null
            ? await mongoQuery.CountAsync(cancellationToken).ConfigureAwait(false)
            : query.Count();
    }

    protected virtual async Task<IReadOnlyCollection<TReadModel>> QueryPaged(EntityPagedQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        var entityQuery = request.Query;

        var queryable = query
            .Sort(entityQuery.Sort);

        if (entityQuery.Page > 0 && entityQuery.PageSize > 0)
            queryable = queryable.Page(entityQuery.Page, entityQuery.PageSize);

        var mongoQuery = queryable as IMongoQueryable<TEntity>;

        var results = mongoQuery != null
            ? await mongoQuery.ToListAsync(cancellationToken).ConfigureAwait(false)
            : queryable.ToList();

        return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
    }
}
