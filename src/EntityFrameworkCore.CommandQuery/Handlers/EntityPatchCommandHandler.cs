using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Commands;
using EntityFrameworkCore.CommandQuery.Definitions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityPatchCommandHandler<TContext, TEntity, TKey, TReadModel>
        : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityPatchCommand<TKey, TReadModel>, TReadModel>
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TContext : DbContext
    {
        public EntityPatchCommandHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }

        protected override async Task<TReadModel> Process(EntityPatchCommand<TKey, TReadModel> message, CancellationToken cancellationToken)
        {
            var dbSet = DataContext
                .Set<TEntity>();

            var keyValue = new object[] { message.Id };

            // find entity to update by message id, not model id
            var entity = await dbSet
                .FindAsync(keyValue, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default(TReadModel);

            // save original for later pipeline processing
            message.Original = await Read(entity.Id, cancellationToken)
                .ConfigureAwait(false);

            // apply json patch to entity
            var jsonPatch = new JsonPatchDocument(
                message.Patch.GetOperations().ToList(),
                message.Patch.ContractResolver);

            jsonPatch.ApplyTo(entity);

            await DataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var model = await Read(entity.Id, cancellationToken)
                .ConfigureAwait(false);

            return model;
        }
    }
}