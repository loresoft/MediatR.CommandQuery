using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityUpdateCommandHandler<TContext, TKey, TEntity, TUpdateModel, TReadModel>
        : RequestHandlerBase<EntityUpdateCommand<TKey, TEntity, TUpdateModel, TReadModel>, TReadModel>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public EntityUpdateCommandHandler(ILoggerFactory loggerFactory, TContext context, IMapper mapper) : base(loggerFactory)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<TReadModel> Process(EntityUpdateCommand<TKey, TEntity, TUpdateModel, TReadModel> message, CancellationToken cancellationToken)
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

            // copy updates from model to entity
            _mapper.Map(message.Model, entity);

            // save updates
            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var readModel = _mapper.Map<TReadModel>(entity);

            return readModel;
        }
    }
}