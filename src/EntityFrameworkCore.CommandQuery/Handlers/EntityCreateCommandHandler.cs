using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityCreateCommandHandler<TContext, TEntity, TKey, TCreateModel, TReadModel>
        : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityCreateCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }

        protected override async Task<TReadModel> Process(EntityCreateCommand<TCreateModel, TReadModel> message, CancellationToken cancellationToken)
        {
            // create new entity from model
            var entity = Mapper.Map<TEntity>(message.Model);

            var dbSet = DataContext
                .Set<TEntity>();

            // add to data set, id should be generated
            await dbSet
                .AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            // save to database
            await DataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // convert to read model
            var readModel = await Read(entity.Id, cancellationToken)
                .ConfigureAwait(false);

            return readModel;
        }
    }
}