using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers
{
    public class EntitySelectQueryHandler<TRepository, TEntity, TKey, TReadModel>
        : RepositoryHandlerBase<TRepository, TEntity, TKey, EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>
        where TRepository : IMongoRepository<TEntity, TKey>
        where TEntity : class
    {
        public EntitySelectQueryHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
            : base(loggerFactory, repository, mapper)
        {
        }

        protected override Task<IReadOnlyCollection<TReadModel>> Process(EntitySelectQuery<TReadModel> request, CancellationToken cancellationToken)
        {
            var query = Repository.All();

            // build query from filter
            query = BuildQuery(request, query) as IOrderedQueryable<TEntity>;

            // page the query and convert to read model
            var result = QueryList(request, query, cancellationToken);

            //TODO make async?
            return Task.FromResult(result);
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

        protected virtual IReadOnlyCollection<TReadModel> QueryList(EntitySelectQuery<TReadModel> request, IQueryable<TEntity> query, CancellationToken cancellationToken)
        {
            var results = query
                .Sort(request?.Select?.Sort)
                .ToList();

            return Mapper.Map<IList<TEntity>, IReadOnlyCollection<TReadModel>>(results);
        }

    }
}
