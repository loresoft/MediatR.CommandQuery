using System.Collections.Generic;
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
    public class EntitySelectQueryHandler<TContext, TEntity, TReadModel>
        : DataContextHandlerBase<TContext, EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>
        where TEntity : class
        where TContext : DbContext
    {
        public EntitySelectQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }

        protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntitySelectQuery<TReadModel> message, CancellationToken cancellationToken)
        {
            // build query from filter
            var result = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Filter(message.Filter)
                .Sort(message.Sort)
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return result.AsReadOnly();
        }
    }
}