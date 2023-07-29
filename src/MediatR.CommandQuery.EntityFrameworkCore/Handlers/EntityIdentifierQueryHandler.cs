using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntityIdentifierQueryHandler<TContext, TEntity, TKey, TReadModel>
    : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityIdentifierQuery<TKey, TReadModel>, TReadModel>
    where TContext : DbContext
    where TEntity : class, IHaveIdentifier<TKey>, new()
{

    public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {
    }


    protected override async Task<TReadModel> Process(EntityIdentifierQuery<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        return await Read(request.Id, cancellationToken).ConfigureAwait(false);
    }
}
