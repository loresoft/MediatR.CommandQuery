using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntityPatchCommandHandler<TContext, TEntity, TKey, TReadModel>
    : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityPatchCommand<TKey, TReadModel>, TReadModel>
    where TContext : DbContext
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityPatchCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {
    }

    protected override async Task<TReadModel> Process(EntityPatchCommand<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var dbSet = DataContext
            .Set<TEntity>();

        var keyValue = new object[] { request.Id };

        // find entity to update by message id, not model id
        var entity = await dbSet
            .FindAsync(keyValue, cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
            return default!;

        // apply json patch to entity
        var jsonPatch = request.Patch;
        jsonPatch.ApplyTo(entity);

        // apply update metadata
        if (entity is ITrackUpdated updateEntity)
        {
            updateEntity.Updated = request.Activated;
            updateEntity.UpdatedBy = request.ActivatedBy;
        }

        await DataContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        // return read model
        var model = await Read(entity.Id, cancellationToken).ConfigureAwait(false);

        return model;
    }
}
