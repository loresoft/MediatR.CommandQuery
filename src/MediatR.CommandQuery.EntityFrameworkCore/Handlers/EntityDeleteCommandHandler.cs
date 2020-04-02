using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers
{
    public class EntityDeleteCommandHandler<TContext, TEntity, TKey, TReadModel>
        : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityDeleteCommand<TKey, TReadModel>, TReadModel>
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TContext : DbContext
    {
        public EntityDeleteCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }

        protected override async Task<TReadModel> Process(EntityDeleteCommand<TKey, TReadModel> request, CancellationToken cancellationToken)
        {
            var dbSet = DataContext
                .Set<TEntity>();

            var keyValue = new object[] { request.Id };

            var entity = await dbSet
                .FindAsync(keyValue, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default(TReadModel);

            // entity supports soft delete
            if (entity is ITrackDeleted deleteEntity)
                deleteEntity.IsDeleted = true;
            else
                dbSet.Remove(entity);

            await DataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // convert deleted entity to read model
            var model = Mapper.Map<TReadModel>(entity);

            return model;
        }

    }
}