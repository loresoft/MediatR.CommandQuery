using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public class EntityIdentifiersQueryHandler<TContext, TEntity, TKey, TReadModel>
    : EntityDataContextHandlerBase<TContext, TEntity, TKey, TReadModel, EntityIdentifiersQuery<TKey, TReadModel>, IResult<IReadOnlyCollection<TReadModel>>>
    where TContext : DbContext
    where TEntity : class, IHaveIdentifier<TKey>, new()
{

    public EntityIdentifiersQueryHandler(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory, dataContext, mapper)
    {
    }


    protected override async Task<IResult<IReadOnlyCollection<TReadModel>>> Process(EntityIdentifiersQuery<TKey, TReadModel> request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var models = await DataContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(p => request.Ids.Contains(p.Id))
            .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(models);
    }
}
