using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR.CommandQuery.Definitions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers
{
    public abstract class EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, TRequest, TResponse>
        : DataContextHandlerBase<TContext, TRequest, TResponse>
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TRequest : IRequest<TResponse>
    {
        protected EntityDataContextHandlerBase(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
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