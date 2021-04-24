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
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers
{
    public class EntitySelectQueryHandler<TRepository, TEntity, TReadModel>
        : RepositoryHandlerBase<TRepository, TEntity, EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class
    {
        public EntitySelectQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
            : base(loggerFactory, repository, mapper)
        {
        }

        protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntitySelectQuery<TReadModel> request, CancellationToken cancellationToken)
        {
            var query = await Repository
                .GetQueryableAsync()
                .ConfigureAwait(false);

            // build query from filter
            query = BuildQuery(request, query) as IOrderedQueryable<TEntity>;

            // page the query and convert to read model
            var result = await QueryList(request, query, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }


        protected virtual IQueryable<TEntity> BuildQuery(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query)
        {
            var entitySelect = request?.Select;

            // build query from filter
            if (entitySelect?.Filter != null)
                query = query.Filter(request.Select.Filter);

            // add raw query
            if (!string.IsNullOrEmpty(entitySelect?.Query))
                query = query.Where(entitySelect.Query);

            return query;
        }

        protected virtual async Task<IReadOnlyCollection<TReadModel>> QueryList(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
        {
            var results = await query
                .Sort(request?.Select?.Sort)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
        }

    }
}