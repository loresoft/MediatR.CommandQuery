using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.CommandQuery.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.CommandQuery.Handlers
{
    public class EntityCreateCommandHandler<TContext, TEntity, TCreateModel, TReadModel>
        : RequestHandlerBase<EntityCreateCommand<TEntity, TCreateModel, TReadModel>, TReadModel>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public EntityCreateCommandHandler(ILoggerFactory loggerFactory, TContext context, IMapper mapper) : base(loggerFactory)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task<TReadModel> Process(EntityCreateCommand<TEntity, TCreateModel, TReadModel> message, CancellationToken cancellationToken)
        {
            // create new entity from model
            var entity = _mapper.Map<TEntity>(message.Model);

            var dbSet = _context
                .Set<TEntity>();

            // add to data set, id should be generated
            await dbSet
                .AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            // save to database
            await _context
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // convert to read model
            var readModel = _mapper.Map<TReadModel>(entity);

            return readModel;
        }
    }
}