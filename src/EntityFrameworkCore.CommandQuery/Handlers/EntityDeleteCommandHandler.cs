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
    public class EntityDeleteCommandHandler<TContext, TKey, TEntity, TReadModel>
        : RequestHandlerBase<EntityDeleteCommand<TKey, TEntity, TReadModel>, TReadModel>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public EntityDeleteCommandHandler(ILoggerFactory loggerFactory, TContext context, IMapper mapper) : base(loggerFactory)
        {
            _context = context;
            _mapper = mapper;
        }


        protected override async Task<TReadModel> Process(EntityDeleteCommand<TKey, TEntity, TReadModel> message, CancellationToken cancellationToken)
        {
            var dbSet = _context
                .Set<TEntity>();

            var keyValue = new object[] { message.Id };

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

            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // convert deleted entity to read model
            var model = _mapper.Map<TReadModel>(entity);

            return model;
        }

    }
}