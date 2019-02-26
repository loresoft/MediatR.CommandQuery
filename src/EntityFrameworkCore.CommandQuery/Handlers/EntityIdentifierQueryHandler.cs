using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityIdentifierQueryHandler<TContext, TEntity, TKey, TReadModel>
        : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityIdentifierQuery<TKey, TReadModel>, TReadModel>
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TContext : DbContext
    {

        public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }


        protected override async Task<TReadModel> Process(EntityIdentifierQuery<TKey, TReadModel> message, CancellationToken cancellationToken)
        {
            var model = await Read(message.Id, cancellationToken)
                .ConfigureAwait(false);

            return model;
        }
    }
}
