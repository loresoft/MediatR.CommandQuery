using Cosmos.Abstracts;

using MediatR.CommandQuery.Behaviors;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Cosmos.Handlers;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Extensions;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Results;
using MediatR.CommandQuery.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery.Cosmos;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddEntityQueries<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TReadModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard queries
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<string, TReadModel>, IResult<TReadModel>>, EntityIdentifierQueryHandler<TRepository, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<string, TReadModel>, IResult<IReadOnlyCollection<TReadModel>>>, EntityIdentifiersQueryHandler<TRepository, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntityPagedQuery<TReadModel>, IResult<EntityPagedResult<TReadModel>>>, EntityPagedQueryHandler<TRepository, TEntity, TReadModel>>();
        services.TryAddTransient<IRequestHandler<EntitySelectQuery<TReadModel>, IResult<IReadOnlyCollection<TReadModel>>>, EntitySelectQueryHandler<TRepository, TEntity, TReadModel>>();

        // pipeline registration, run in order registered
        bool supportsTenant = typeof(TReadModel).Implements<IHaveTenant<string>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, TenantPagedQueryBehavior<string, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, TenantSelectQueryBehavior<string, TReadModel>>();
        }

        bool supportsDeleted = typeof(TReadModel).Implements<ITrackDeleted>();
        if (supportsDeleted)
        {
            services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, DeletedPagedQueryBehavior<TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, DeletedSelectQueryBehavior<TReadModel>>();
        }

        return services;
    }

    public static IServiceCollection AddEntityQueryMemoryCache<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<string, TReadModel>, TReadModel>, MemoryCacheQueryBehavior<EntityIdentifierQuery<string, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<string, TReadModel>, IReadOnlyCollection<TReadModel>>, MemoryCacheQueryBehavior<EntityIdentifiersQuery<string, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, MemoryCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, MemoryCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        return services;
    }

    public static IServiceCollection AddEntityQueryDistributedCache<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<string, TReadModel>, TReadModel>, DistributedCacheQueryBehavior<EntityIdentifierQuery<string, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<string, TReadModel>, IReadOnlyCollection<TReadModel>>, DistributedCacheQueryBehavior<EntityIdentifiersQuery<string, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, DistributedCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, DistributedCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        return services;
    }


    public static IServiceCollection AddEntityCommands<TRepository, TEntity, TReadModel, TCreateModel, TUpdateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TCreateModel : class
        where TUpdateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.TryAddSingleton<IPrincipalReader, PrincipalReader>();

        services
            .AddEntityCreateCommand<TRepository, TEntity, TReadModel, TCreateModel>()
            .AddEntityUpdateCommand<TRepository, TEntity, TReadModel, TUpdateModel>()
            .AddEntityUpsertCommand<TRepository, TEntity, TReadModel, TUpdateModel>()
            .AddEntityPatchCommand<TRepository, TEntity, TReadModel>()
            .AddEntityDeleteCommand<TRepository, TEntity, TReadModel>();

        return services;
    }


    public static IServiceCollection AddEntityCreateCommand<TRepository, TEntity, TReadModel, TCreateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TCreateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityCreateCommand<TCreateModel, TReadModel>, IResult<TReadModel>>, EntityCreateCommandHandler<TRepository, TEntity, TCreateModel, TReadModel>>();

        // pipeline registration, run in order registered
        var createType = typeof(TCreateModel);
        bool supportsTenant = createType.Implements<IHaveTenant<string>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, TenantDefaultCommandBehavior<string, TCreateModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, TenantAuthenticateCommandBehavior<string, TCreateModel, TReadModel>>();
        }

        bool supportsTracking = createType.Implements<ITrackCreated>();
        if (supportsTracking)
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, TrackChangeCommandBehavior<TCreateModel, TReadModel>>();

        services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, ValidateEntityModelCommandBehavior<TCreateModel, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<string, TCreateModel, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityUpdateCommand<TRepository, TEntity, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TUpdateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // allow query for update models
        services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<string, TUpdateModel>, IResult<TUpdateModel>>, EntityIdentifierQueryHandler<TRepository, TEntity, TUpdateModel>>();
        services.TryAddTransient<IRequestHandler<EntityIdentifiersQuery<string, TUpdateModel>, IResult<IReadOnlyCollection<TUpdateModel>>>, EntityIdentifiersQueryHandler<TRepository, TEntity, TUpdateModel>>();

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpdateCommand<string, TUpdateModel, TReadModel>, IResult<TReadModel>>, EntityUpdateCommandHandler<TRepository, TEntity, TUpdateModel, TReadModel>>();

        // pipeline registration, run in order registered
        var updateType = typeof(TUpdateModel);
        bool supportsTenant = updateType.Implements<IHaveTenant<string>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityUpdateCommand<string, TUpdateModel, TReadModel>, TReadModel>, TenantDefaultCommandBehavior<string, TUpdateModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityUpdateCommand<string, TUpdateModel, TReadModel>, TReadModel>, TenantAuthenticateCommandBehavior<string, TUpdateModel, TReadModel>>();
        }

        bool supportsTracking = updateType.Implements<ITrackUpdated>();
        if (supportsTracking)
            services.AddTransient<IPipelineBehavior<EntityUpdateCommand<string, TUpdateModel, TReadModel>, TReadModel>, TrackChangeCommandBehavior<TUpdateModel, TReadModel>>();

        services.AddTransient<IPipelineBehavior<EntityUpdateCommand<string, TUpdateModel, TReadModel>, TReadModel>, ValidateEntityModelCommandBehavior<TUpdateModel, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityUpdateCommand<string, TUpdateModel, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<string, TUpdateModel, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityUpsertCommand<TRepository, TEntity, TReadModel, TUpdateModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
        where TUpdateModel : class
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityUpsertCommand<string, TUpdateModel, TReadModel>, IResult<TReadModel>>, EntityUpsertCommandHandler<TRepository, TEntity, TUpdateModel, TReadModel>>();

        // pipeline registration, run in order registered
        var updateType = typeof(TUpdateModel);
        bool supportsTenant = updateType.Implements<IHaveTenant<string>>();
        if (supportsTenant)
        {
            services.AddTransient<IPipelineBehavior<EntityUpsertCommand<string, TUpdateModel, TReadModel>, TReadModel>, TenantDefaultCommandBehavior<string, TUpdateModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityUpsertCommand<string, TUpdateModel, TReadModel>, TReadModel>, TenantAuthenticateCommandBehavior<string, TUpdateModel, TReadModel>>();
        }

        bool supportsTracking = updateType.Implements<ITrackUpdated>();
        if (supportsTracking)
            services.AddTransient<IPipelineBehavior<EntityUpsertCommand<string, TUpdateModel, TReadModel>, TReadModel>, TrackChangeCommandBehavior<TUpdateModel, TReadModel>>();

        services.AddTransient<IPipelineBehavior<EntityUpsertCommand<string, TUpdateModel, TReadModel>, TReadModel>, ValidateEntityModelCommandBehavior<TUpdateModel, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityUpsertCommand<string, TUpdateModel, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<string, TUpdateModel, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityPatchCommand<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityPatchCommand<string, TReadModel>, IResult<TReadModel>>, EntityPatchCommandHandler<TRepository, TEntity, TReadModel>>();

        // pipeline registration, run in order registered
        services.AddTransient<IPipelineBehavior<EntityPatchCommand<string, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<string, TEntity, TReadModel>>();

        return services;
    }

    public static IServiceCollection AddEntityDeleteCommand<TRepository, TEntity, TReadModel>(this IServiceCollection services)
        where TRepository : ICosmosRepository<TEntity>
        where TEntity : class, IHaveIdentifier<string>, new()
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        // standard crud commands
        services.TryAddTransient<IRequestHandler<EntityDeleteCommand<string, TReadModel>, IResult<TReadModel>>, EntityDeleteCommandHandler<TRepository, TEntity, TReadModel>>();

        // pipeline registration, run in order registered
        services.AddTransient<IPipelineBehavior<EntityDeleteCommand<string, TReadModel>, TReadModel>, EntityChangeNotificationBehavior<string, TEntity, TReadModel>>();

        return services;
    }
}
