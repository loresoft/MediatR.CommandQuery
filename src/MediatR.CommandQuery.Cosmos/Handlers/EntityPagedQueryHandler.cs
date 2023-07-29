using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Cosmos.Abstracts;
using Cosmos.Abstracts.Extensions;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;

using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public class EntityPagedQueryHandler<TRepository, TEntity, TReadModel>
    : RepositoryHandlerBase<TRepository, TEntity, EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>
    where TRepository : ICosmosRepository<TEntity>
    where TEntity : class
{
    public EntityPagedQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
        : base(loggerFactory, repository, mapper)
    {
    }

    protected override async Task<EntityPagedResult<TReadModel>> Process(EntityPagedQuery<TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var cosmosQuery = await Repository
            .GetQueryableAsync()
            .ConfigureAwait(false);

        // build query from filter
        var query = BuildQuery(request, cosmosQuery);

        // get total for query
        int total = await QueryTotal(request, query, cancellationToken)
            .ConfigureAwait(false);

        // short circuit if total is zero
        if (total == 0)
            return new EntityPagedResult<TReadModel> { Data = new List<TReadModel>() };

        // page the query and convert to read model
        var result = await QueryPaged(request, query, cancellationToken)
            .ConfigureAwait(false);

        return new EntityPagedResult<TReadModel>
        {
            Total = total,
            Data = result
        };
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
        return await query
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    protected virtual async Task<IReadOnlyCollection<TReadModel>> QueryPaged(EntityPagedQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        var entityQuery = request.Query;

        var queryable = query
            .Sort(entityQuery.Sort);

        if (entityQuery.Page > 0 && entityQuery.PageSize > 0)
            queryable = queryable.Page(entityQuery.Page, entityQuery.PageSize);

        var results = await queryable
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
    }
}
