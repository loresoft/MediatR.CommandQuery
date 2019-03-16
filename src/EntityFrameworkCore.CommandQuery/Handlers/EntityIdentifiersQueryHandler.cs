using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityIdentifiersQueryHandler<TContext, TEntity, TKey, TReadModel>
        : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TContext : DbContext
    {

        public EntityIdentifiersQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }


        protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntityIdentifiersQuery<TKey, TReadModel> message, CancellationToken cancellationToken)
        {
            var model = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => message.Ids.Contains(p.Id))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return model;
        }
    }
}