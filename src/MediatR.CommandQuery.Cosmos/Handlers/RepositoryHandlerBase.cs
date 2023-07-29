using System;

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
        if (repository is null)
            throw new ArgumentNullException(nameof(repository));
        if (mapper is null)
            throw new ArgumentNullException(nameof(mapper));

        Repository = repository;
        Mapper = mapper;
    }

    protected TRepository Repository { get; }

    protected IMapper Mapper { get; }
}
