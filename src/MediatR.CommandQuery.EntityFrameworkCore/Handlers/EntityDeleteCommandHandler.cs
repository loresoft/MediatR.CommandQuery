using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntityDeleteCommandHandler<TContext, TEntity, TKey, TReadModel>
    : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityDeleteCommand<TKey, TReadModel>, TReadModel>
    where TContext : DbContext
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityDeleteCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {
    }

    protected override async Task<TReadModel> Process(EntityDeleteCommand<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var dbSet = DataContext
            .Set<TEntity>();

        var keyValue = new object[] { request.Id };

        var entity = await dbSet
            .FindAsync(keyValue, cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
            return default!;

        // apply update metadata
        if (entity is ITrackUpdated updateEntity)
        {
            updateEntity.UpdatedBy = request.ActivatedBy;
            updateEntity.Updated = request.Activated;
        }

        // entity supports soft delete
        if (entity is ITrackDeleted deleteEntity)
        {
            deleteEntity.IsDeleted = true;
        }
        else
        {
            // when history is tracked, need to update the entity with update metadata before deleting
            if (entity is ITrackHistory and ITrackUpdated)
            {
                await DataContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            // delete the entity
            dbSet.Remove(entity);
        }

        await DataContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        // convert deleted entity to read model
        var model = Mapper.Map<TReadModel>(entity);

        return model;
    }

}
