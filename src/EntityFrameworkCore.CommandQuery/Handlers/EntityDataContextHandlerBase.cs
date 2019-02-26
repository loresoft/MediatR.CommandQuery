using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.CommandQuery.Definitions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public abstract class EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, TRequest, TResponse>
        : RequestHandlerBase<TRequest, TResponse>
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TRequest : IRequest<TResponse>
    {
        protected TContext DataContext { get; }
        protected IMapper Mapper { get; }

        protected EntityDataContextHandlerBase(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory)
        {
            DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected virtual async Task<TReadModel> Read(TKey key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => Equals(p.Id, key))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            return model;
        }
    }
}