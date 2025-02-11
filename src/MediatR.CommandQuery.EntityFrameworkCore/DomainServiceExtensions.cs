using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.EntityFrameworkCore.Handlers;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery.EntityFrameworkCore;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddEntityQueries<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TReadModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        CacheTagger.SetTag<TReadModel, TEntity>();

        // standard queries
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>, EntityIdentifierQueryHandler<TContext, TEntity, TKey, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>, EntityIdentifiersQueryHandler<TContext, TEntity, TKey, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, EntityPagedQueryHandler<TContext, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, EntitySelectQueryHandler<TContext, TEntity, TReadModel>>();

        services.AddEntityQueryBehaviors<TKey, TReadModel>();

        return services;
    }


    public static IServiceCollection AddEntityCommands<TContext, TEntity, TKey, TReadModel, TCreateModel, TUpdateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TCreateModel : class
        where TUpdateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        CacheTagger.SetTag<TReadModel, TEntity>();
        CacheTagger.SetTag<TCreateModel, TEntity>();
        CacheTagger.SetTag<TUpdateModel, TEntity>();

        services
            .AddEntityCreateCommand<TContext, TEntity, TKey, TReadModel, TCreateModel>()
            .AddEntityUpdateCommand<TContext, TEntity, TKey, TReadModel, TUpdateModel>()
            .AddEntityUpsertCommand<TContext, TEntity, TKey, TReadModel, TUpdateModel>()
            .AddEntityPatchCommand<TContext, TEntity, TKey, TReadModel>()
            .AddEntityDeleteCommand<TContext, TEntity, TKey, TReadModel>();

        return services;
    }


    public static IServiceCollection AddEntityCreateCommand<TContext, TEntity, TKey, TReadModel, TCreateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TCreateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, EntityCreateCommandHandler<TContext, TEntity, TKey, TCreateModel, TReadModel>>();

        services.AddEntityCreateBehaviors<TKey, TReadModel, TCreateModel>();

        return services;
    }

    public static IServiceCollection AddEntityUpdateCommand<TContext, TEntity, TKey, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TUpdateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // allow query for update models
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<TKey, TUpdateModel>, TUpdateModel>, EntityIdentifierQueryHandler<TContext, TEntity, TKey, TUpdateModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<TKey, TUpdateModel>, IReadOnlyCollection<TUpdateModel>>, EntityIdentifiersQueryHandler<TContext, TEntity, TKey, TUpdateModel>>();

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, EntityUpdateCommandHandler<TContext, TEntity, TKey, TUpdateModel, TReadModel>>();

        services.AddEntityUpdateBehaviors<TKey, TReadModel, TUpdateModel>();

        return services;
    }

    public static IServiceCollection AddEntityUpsertCommand<TContext, TEntity, TKey, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TUpdateModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, EntityUpsertCommandHandler<TContext, TEntity, TKey, TUpdateModel, TReadModel>>();

        services.AddEntityUpsertBehaviors<TKey, TReadModel, TUpdateModel>();

        return services;
    }

    public static IServiceCollection AddEntityPatchCommand<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityPatchCommand<TKey, TReadModel>, TReadModel>, EntityPatchCommandHandler<TContext, TEntity, TKey, TReadModel>>();

        services.AddEntityPatchBehaviors<TKey, TEntity, TReadModel>();

        return services;
    }

    public static IServiceCollection AddEntityDeleteCommand<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(services);

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityDeleteCommand<TKey, TReadModel>, TReadModel>, EntityDeleteCommandHandler<TContext, TEntity, TKey, TReadModel>>();

        services.AddEntityDeleteBehaviors<TKey, TEntity, TReadModel>();

        return services;
    }

}
