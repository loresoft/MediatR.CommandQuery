using MediatR.CommandQuery.Behaviors;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.EntityFrameworkCore.Handlers;
using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;
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
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard queries
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<TKey, TReadModel>, IResult<TReadModel>>, EntityIdentifierQueryHandler<TContext, TEntity, TKey, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<TKey, TReadModel>, IResult<IReadOnlyCollection<TReadModel>>>, EntityIdentifiersQueryHandler<TContext, TEntity, TKey, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityPagedQuery<TReadModel>, IResult<EntityPagedResult<TReadModel>>>, EntityPagedQueryHandler<TContext, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntitySelectQuery<TReadModel>, IResult<IReadOnlyCollection<TReadModel>>>, EntitySelectQueryHandler<TContext, TEntity, TReadModel>>();

        // pipeline registration, run in order registered
        bool supportsTenant = typeof(IResult<TReadModel>).Implements<IHaveTenant<TKey>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, TenantPagedQueryBehavior<TKey, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, TenantSelectQueryBehavior<TKey, TReadModel>>();
        }

        bool supportsDeleted = typeof(IResult<TReadModel>).Implements<ITrackDeleted>();
        if (supportsDeleted)
        {
            services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, DeletedPagedQueryBehavior<TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, DeletedSelectQueryBehavior<TReadModel>>();
        }

        return services;
    }

    public static IServiceCollection AddEntityQueryMemoryCache<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>, MemoryCacheQueryBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>, MemoryCacheQueryBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, MemoryCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, MemoryCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        return services;
    }

    public static IServiceCollection AddEntityQueryDistributedCache<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>, DistributedCacheQueryBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>, DistributedCacheQueryBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, DistributedCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, DistributedCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        return services;
    }


    public static IServiceCollection AddEntityCommands<TContext, TEntity, TKey, TReadModel, TCreateModel, TUpdateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TCreateModel : class
        where TUpdateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.TryAddSingleton<IPrincipalReader, PrincipalReader>();

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
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityCreateCommand<TCreateModel, TReadModel>, IResult<TReadModel>>, EntityCreateCommandHandler<TContext, TEntity, TKey, TCreateModel, TReadModel>>();

        // pipeline registration, run in order registered
        var createType = typeof(TCreateModel);
        bool supportsTenant = createType.Implements<IHaveTenant<TKey>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, TenantDefaultCommandBehavior<TKey, TCreateModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, TenantAuthenticateCommandBehavior<TKey, TCreateModel, TReadModel>>();
        }

        bool supportsTracking = createType.Implements<ITrackCreated>();
        if (supportsTracking)
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, TrackChangeCommandBehavior<TCreateModel, TReadModel>>();

        services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, ValidateEntityModelCommandBehavior<TCreateModel, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<TKey, TCreateModel, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityUpdateCommand<TContext, TEntity, TKey, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TUpdateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // allow query for update models
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<TKey, TUpdateModel>, IResult<TUpdateModel>>, EntityIdentifierQueryHandler<TContext, TEntity, TKey, TUpdateModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<TKey, TUpdateModel>, IResult<IReadOnlyCollection<TUpdateModel>>>, EntityIdentifiersQueryHandler<TContext, TEntity, TKey, TUpdateModel>>();

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, IResult<TReadModel>>, EntityUpdateCommandHandler<TContext, TEntity, TKey, TUpdateModel, TReadModel>>();

        // pipeline registration, run in order registered
        var updateType = typeof(TUpdateModel);
        bool supportsTenant = updateType.Implements<IHaveTenant<TKey>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, TenantDefaultCommandBehavior<TKey, TUpdateModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, TenantAuthenticateCommandBehavior<TKey, TUpdateModel, TReadModel>>();
        }

        bool supportsTracking = updateType.Implements<ITrackUpdated>();
        if (supportsTracking)
            services.AddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, TrackChangeCommandBehavior<TUpdateModel, TReadModel>>();

        services.AddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, ValidateEntityModelCommandBehavior<TUpdateModel, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<TKey, TUpdateModel, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityUpsertCommand<TContext, TEntity, TKey, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TUpdateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, IResult<TReadModel>>, EntityUpsertCommandHandler<TContext, TEntity, TKey, TUpdateModel, TReadModel>>();

        // pipeline registration, run in order registered
        var updateType = typeof(TUpdateModel);
        bool supportsTenant = updateType.Implements<IHaveTenant<TKey>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, TenantDefaultCommandBehavior<TKey, TUpdateModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, TenantAuthenticateCommandBehavior<TKey, TUpdateModel, TReadModel>>();
        }

        bool supportsTracking = updateType.Implements<ITrackUpdated>();
        if (supportsTracking)
            services.AddTransient<IPipelineBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, TrackChangeCommandBehavior<TUpdateModel, TReadModel>>();

        services.AddTransient<IPipelineBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, ValidateEntityModelCommandBehavior<TUpdateModel, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<TKey, TUpdateModel, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityPatchCommand<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityPatchCommand<TKey, TReadModel>, IResult<TReadModel>>, EntityPatchCommandHandler<TContext, TEntity, TKey, TReadModel>>();

        // pipeline registration, run in order registered
        services.AddTransient<IPipelineBehavior<EntityPatchCommand<TKey, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<TKey, TEntity, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityDeleteCommand<TContext, TEntity, TKey, TReadModel>(this IServiceCollection services)
        where TContext : DbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityDeleteCommand<TKey, TReadModel>, IResult<TReadModel>>, EntityDeleteCommandHandler<TContext, TEntity, TKey, TReadModel>>();

        // pipeline registration, run in order registered
        services.AddTransient<IPipelineBehavior<EntityDeleteCommand<TKey, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<TKey, TEntity, TReadModel>>();

        return services;
    }
}
