using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers
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

        protected override async Task<TReadModel> Process(EntityCreateCommand<TCreateModel, TReadModel> request, CancellationToken cancellationToken)
        {
            // create new entity from model
            var entity = Mapper.Map<TEntity>(request.Model);

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