using AutoMapper;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntityIdentifierQueryHandler<TContext, TEntity, TKey, TReadModel>
    : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityIdentifierQuery<TKey, TReadModel>, IResult<TReadModel>>
    where TContext : DbContext
    where TEntity : class, IHaveIdentifier<TKey>, new()
{

    public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {
    }


    protected override async Task<IResult<TReadModel>> Process(EntityIdentifierQuery<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        var result = await Read(request.Id, cancellationToken).ConfigureAwait(false);
        return Result.Ok(result);
    }
}
