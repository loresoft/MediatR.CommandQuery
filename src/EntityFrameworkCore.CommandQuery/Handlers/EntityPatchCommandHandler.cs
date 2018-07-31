using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityPatchCommandHandler<TContext, TKey, TEntity, TReadModel>
        : RequestHandlerBase<EntityPatchCommand<TKey, TEntity, TReadModel>, TReadModel>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public EntityPatchCommandHandler(ILoggerFactory loggerFactory, TContext context, IMapper mapper) : base(loggerFactory)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<TReadModel> Process(EntityPatchCommand<TKey, TEntity, TReadModel> message, CancellationToken cancellationToken)
        {
            var dbSet = _context
                .Set<TEntity>();

            var keyValue = new object[] { message.Id };

            // find entity to update by message id, not model id
            var entity = await dbSet
                .FindAsync(keyValue, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default(TReadModel);

            // save original for later pipeline processing
            message.Original = _mapper.Map<TReadModel>(entity);

            // apply json patch to entity
            message.Patch.ApplyTo(entity);

            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var model = _mapper.Map<TReadModel>(entity);

            return model;
        }
    }
}