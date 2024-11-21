using System.Security.Claims;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Queries;

namespace MediatR.CommandQuery.Dispatcher;

public class DispatcherDataService : IDispatcherDataService
{
    public DispatcherDataService(IDispatcher dispatcher)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);

        Dispatcher = dispatcher;
    }


    public IDispatcher Dispatcher { get; }


    public async Task<TModel?> Get<TKey, TModel>(
        TKey id,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntityIdentifierQuery<TKey, TModel>(user, id);
        command.Cache(cacheTime);

        return await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyCollection<TModel>> Get<TKey, TModel>(
        IEnumerable<TKey> ids,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntityIdentifiersQuery<TKey, TModel>(user, ids);
        command.Cache(cacheTime);

        var result = await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
        return result ?? [];
    }

    public async Task<IReadOnlyCollection<TModel>> All<TModel>(
        string? sortField = null,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class
    {
        var filter = new EntityFilter();
        var sort = EntitySort.Parse(sortField);

        var select = new EntitySelect(filter, sort);

        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntitySelectQuery<TModel>(user, select);
        command.Cache(cacheTime);

        var result = await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
        return result ?? [];
    }

    public async Task<IReadOnlyCollection<TModel>> Select<TModel>(
        EntitySelect? entitySelect = null,
        TimeSpan? cacheTime = null,
        CancellationToken cancellationToken = default)
        where TModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntitySelectQuery<TModel>(user, entitySelect);
        command.Cache(cacheTime);

        var result = await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
        return result ?? [];
    }

    public async Task<EntityPagedResult<TModel>> Page<TModel>(
        EntityQuery? entityQuery = null,
        CancellationToken cancellationToken = default)
        where TModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntityPagedQuery<TModel>(user, entityQuery);

        var result = await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);

        return result ?? new EntityPagedResult<TModel>();
    }


    public async Task<IEnumerable<TModel>> Search<TModel>(
        string searchText,
        CancellationToken cancellationToken = default)
        where TModel : class, ISupportSearch
    {
        var filter = EntityFilterBuilder.CreateSearchFilter(TModel.SearchFields(), searchText);
        var sort = new EntitySort { Name = TModel.SortField() };

        var select = new EntitySelect(filter, sort);

        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntitySelectQuery<TModel>(user, select);

        var result = await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
        return result ?? [];
    }


    public async Task<TReadModel?> Save<TKey, TUpdateModel, TReadModel>(
        TKey id,
        TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
        where TReadModel : class
        where TUpdateModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);

        return await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TReadModel?> Create<TCreateModel, TReadModel>(
        TCreateModel createModel,
        CancellationToken cancellationToken = default)
        where TReadModel : class
        where TCreateModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntityCreateCommand<TCreateModel, TReadModel>(user, createModel);

        return await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TReadModel?> Update<TKey, TUpdateModel, TReadModel>(
        TKey id,
        TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
        where TReadModel : class
        where TUpdateModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);

        var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);

        return await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TReadModel?> Delete<TKey, TReadModel>(
        TKey id,
        CancellationToken cancellationToken = default)
        where TReadModel : class
    {
        var user = await GetUser(cancellationToken).ConfigureAwait(false);
        var command = new EntityDeleteCommand<TKey, TReadModel>(user, id);

        return await Dispatcher.Send(command, cancellationToken).ConfigureAwait(false);
    }


    public virtual Task<ClaimsPrincipal?> GetUser(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<ClaimsPrincipal?>(null);
    }

}
