using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers
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


        protected override async Task<IReadOnlyCollection<TReadModel>> Process(EntityIdentifiersQuery<TKey, TReadModel> request, CancellationToken cancellationToken)
        {
            var models = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => request.Ids.Contains(p.Id))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return models;
        }
    }
}