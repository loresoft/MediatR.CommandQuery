using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.CommandQuery.Extensions;
using EntityFrameworkCore.CommandQuery.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityListQueryHandler<TContext, TEntity, TReadModel>
        : RequestHandlerBase<EntityListQuery<TReadModel>, EntityListResult<TReadModel>>
        where TEntity : class
        where TContext : DbContext
    {
        private static readonly Lazy<IReadOnlyCollection<TReadModel>> _emptyList = new Lazy<IReadOnlyCollection<TReadModel>>(() => new List<TReadModel>().AsReadOnly());

        private readonly TContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public EntityListQueryHandler(ILoggerFactory loggerFactory, TContext context, IConfigurationProvider configurationProvider) : base(loggerFactory)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }

        protected override async Task<EntityListResult<TReadModel>> Process(EntityListQuery<TReadModel> message, CancellationToken cancellationToken)
        {
            var entityQuery = message.Query;

            // build query from filter
            var query = _context
                .Set<TEntity>()
                .AsNoTracking()
                .Filter(entityQuery.Filter);

            // add raw query
            if (!string.IsNullOrEmpty(entityQuery.Query))
                query = query.Where(entityQuery.Query);

            // get total for query
            var total = await query
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

            // short circuit if total is zero
            if (total == 0)
                return new EntityListResult<TReadModel> { Data = _emptyList.Value };

            // page the query and convert to read model
            var result = await query
                .Sort(entityQuery.Sort)
                .Page(entityQuery.Page, entityQuery.PageSize)
                .ProjectTo<TReadModel>(_configurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new EntityListResult<TReadModel>
            {
                Total = total,
                Data = result.AsReadOnly()
            };
        }



    }
}