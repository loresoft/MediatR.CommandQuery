using System.Linq.Dynamic.Core;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntitySelectQueryHandler<TContext, TEntity, TReadModel>
    : DataContextHandlerBase<TContext, EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>
    where TContext : DbContext
    where TEntity : class
{
    public EntitySelectQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {
    }


    protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntitySelectQuery<TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var query = DataContext
            .Set<TEntity>()
            .AsNoTracking();

        // build query from filter
        query = BuildQuery(request, query);

        // page the query and convert to read model
        var result = await QueryList(request, query, cancellationToken).ConfigureAwait(false);

        return result;
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

    protected virtual async Task<IReadOnlyCollection<TReadModel>> QueryList(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        return await query
            .Sort(request?.Select?.Sort)
            .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

}
