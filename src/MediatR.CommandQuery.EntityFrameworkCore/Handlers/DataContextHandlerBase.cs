using AutoMapper;

using MediatR.CommandQuery.Handlers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.EntityFrameworkCore.Handlers;

public abstract class DataContextHandlerBase<TContext, TRequest, TResponse>
    : RequestHandlerBase<TRequest, TResponse>
    where TContext : DbContext
    where TRequest : IRequest<TResponse>
{
    protected DataContextHandlerBase(ILoggerFactory loggerFactory, TContext dataContext, IMapper mapper)
        : base(loggerFactory)
    {
        DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    protected TContext DataContext { get; }

    protected IMapper Mapper { get; }
}
