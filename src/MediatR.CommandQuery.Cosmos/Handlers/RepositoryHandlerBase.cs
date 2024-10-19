using AutoMapper;

using Cosmos.Abstracts;

using MediatR.CommandQuery.Handlers;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Cosmos.Handlers;

public abstract class RepositoryHandlerBase<TRepository, TEntity, TRequest, TResponse>
    : RequestHandlerBase<TRequest, TResponse>
    where TRepository : ICosmosRepository<TEntity>
    where TRequest : IRequest<TResponse>
{
    protected RepositoryHandlerBase(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper) : base(loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(mapper);

        Repository = repository;
        Mapper = mapper;
    }

    protected TRepository Repository { get; }

    protected IMapper Mapper { get; }
}
