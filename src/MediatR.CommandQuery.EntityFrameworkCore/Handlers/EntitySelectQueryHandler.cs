using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers
{
    public class EntitySelectQueryHandler<TContext, TEntity, TReadModel>
        : DataContextHandlerBase<TContext, EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>
        where TEntity : class
        where TContext : DbContext
    {
        public EntitySelectQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }


        protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntitySelectQuery<TReadModel> request, CancellationToken cancellationToken)
        {
            var query = DataContext
                .Set<TEntity>()
                .AsNoTracking();

            // build query from filter
            query = BuildQuery(request, query);

            // page the query and convert to read model
            var result = await QueryList(request, query, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }


        protected virtual IQueryable<TEntity> BuildQuery(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query)
        {
            // build query from filter
            if (request?.Filter != null)
                query = query.Filter(request.Filter);

            return query;
        }

        protected virtual async Task<IReadOnlyCollection<TReadModel>> QueryList(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
        {
            return await query
                .Sort(request.Sort)
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

    }
}