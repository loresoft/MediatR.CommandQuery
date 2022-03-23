using System;

using AutoMapper;

using MediatR.CommandQuery.Handlers;

using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers;

public abstract class RepositoryHandlerBase<TRepository, TEntity, TKey, TRequest, TResponse>
    : RequestHandlerBase<TRequest, TResponse>
    where TRepository : IMongoRepository<TEntity, TKey>
    where TRequest : IRequest<TResponse>
    where TEntity : class
{
    protected RepositoryHandlerBase(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper) : base(loggerFactory)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    protected TRepository Repository { get; }

    protected IMapper Mapper { get; }
}
