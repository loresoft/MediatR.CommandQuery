using System.Security.Claims;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

namespace MediatR.CommandQuery.Dispatcher;

public interface IDispatcherDataService
{
    IDispatcher Dispatcher { get; }

    Task<TModel?> Get<TKey, TModel>(
        TKey id,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class;

    Task<IReadOnlyCollection<TModel>> Get<TKey, TModel>(
        IEnumerable<TKey> ids,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class;

    Task<IReadOnlyCollection<TModel>> All<TModel>(
        string? sortField = null,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class;

    Task<IReadOnlyCollection<TModel>> Select<TModel>(
        EntitySelect? entitySelect = null,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class;

    Task<EntityPagedResult<TModel>> Page<TModel>(
        EntityQuery? entityQuery = null,
        CancellationToken cancellationToken = default)
        where TModel : class;


    Task<IEnumerable<TModel>> Search<TModel>(
        string searchText,
        EntityFilter? entityFilter = null,
        CancellationToken cancellationToken = default)
        where TModel : class, ISupportSearch;



    Task<TReadModel?> Save<TKey, TUpdateModel, TReadModel>(
        TKey id,
        TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
        where TUpdateModel : class
        where TReadModel : class;

    Task<TReadModel?> Create<TCreateModel, TReadModel>(
        TCreateModel createModel,
        CancellationToken cancellationToken = default)
        where TCreateModel : class
        where TReadModel : class;

    Task<TReadModel?> Update<TKey, TUpdateModel, TReadModel>(
        TKey id,
        TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
        where TUpdateModel : class
        where TReadModel : class;

    Task<TReadModel?> Delete<TKey, TReadModel>(
        TKey id,
        CancellationToken cancellationToken = default) where TReadModel : class;


    Task<ClaimsPrincipal?> GetUser(CancellationToken cancellationToken = default);
}
