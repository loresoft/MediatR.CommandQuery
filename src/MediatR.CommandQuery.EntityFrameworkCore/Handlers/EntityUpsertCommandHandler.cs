using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Results;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntityUpsertCommandHandler<TContext, TEntity, TKey, TUpdateModel, TReadModel>
    : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, IResult<TReadModel>>
    where TContext : DbContext
    where TEntity : class, IHaveIdentifier<TKey>, new()
{
    public EntityUpsertCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {

    }

    protected override async Task<IResult<TReadModel>> Process(EntityUpsertCommand<TKey, TUpdateModel, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var dbSet = DataContext
            .Set<TEntity>();

        // don't query if default value
        var entity = !EqualityComparer<TKey>.Default.Equals(request.Id, default)
            ? await dbSet.FindAsync([request.Id], cancellationToken).ConfigureAwait(false)
            : default;

        // create entity if not found
        if (entity == null)
        {
            entity = new TEntity();
            entity.Id = request.Id;

            // apply create metadata
            if (entity is ITrackCreated createdModel)
            {
                createdModel.Created = request.Activated;
                createdModel.CreatedBy = request.ActivatedBy;
            }

            await dbSet
                .AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);
        }

        // copy updates from model to entity
        Mapper.Map(request.Model, entity);

        // apply update metadata
        if (entity is ITrackUpdated updateEntity)
        {
            updateEntity.Updated = request.Activated;
            updateEntity.UpdatedBy = request.ActivatedBy;
        }

        // save updates
        await DataContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        // return read model
        var readModel = await Read(entity.Id, cancellationToken).ConfigureAwait(false);

        return Result.Ok(readModel);
    }
}
