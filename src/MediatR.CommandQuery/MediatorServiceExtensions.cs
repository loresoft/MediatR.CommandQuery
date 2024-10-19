using FluentValidation;

using MediatR.CommandQuery.Behaviors;
using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Dispatcher;
using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Services;
using MediatR.NotificationPublishers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery;

public static class MediatorServiceExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAdd(new ServiceDescriptor(typeof(IMediator), typeof(Mediator), lifetime));
        services.TryAdd(new ServiceDescriptor(typeof(ISender), (sp) => sp.GetRequiredService<IMediator>(), lifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IPublisher), (sp) => sp.GetRequiredService<IMediator>(), lifetime));
        services.TryAdd(new ServiceDescriptor(typeof(INotificationPublisher), typeof(TaskWhenAllPublisher), lifetime));

        services.TryAddSingleton<IPrincipalReader, PrincipalReader>();

        return services;
    }

    public static IServiceCollection AddValidatorsFromAssembly<T>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Register validators
        var scanner = AssemblyScanner.FindValidatorsInAssemblyContaining<T>();
        foreach (var scanResult in scanner)
        {
            //Register as interface
            services.TryAdd(new ServiceDescriptor(scanResult.InterfaceType, scanResult.ValidatorType, ServiceLifetime.Singleton));
            //Register as self
            services.TryAdd(new ServiceDescriptor(scanResult.ValidatorType, scanResult.ValidatorType, ServiceLifetime.Singleton));
        }

        return services;
    }


    public static IServiceCollection AddRemoteDispatcher(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddTransient<IDispatcher>(sp => sp.GetRequiredService<RemoteDispatcher>());
        services.AddOptions<DispatcherOptions>();

        return services;
    }

    public static IServiceCollection AddServerDispatcher(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddTransient<IDispatcher, MediatorDispatcher>();
        services.AddOptions<DispatcherOptions>();

        return services;
    }


    public static IServiceCollection AddEntityQueryMemoryCache<TKey, TReadModel>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>, MemoryCacheQueryBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>, MemoryCacheQueryBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, MemoryCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, MemoryCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        return services;
    }

    public static IServiceCollection AddEntityQueryDistributedCache<TKey, TReadModel>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>, DistributedCacheQueryBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>, DistributedCacheQueryBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, DistributedCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, DistributedCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        return services;
    }


    public static IServiceCollection AddEntityHybridCache<TKey, TReadModel, TCreateModel, TUpdateModel>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IPipelineBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>, HybridCacheQueryBehavior<EntityIdentifierQuery<TKey, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>, HybridCacheQueryBehavior<EntityIdentifiersQuery<TKey, TReadModel>, IReadOnlyCollection<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>, HybridCacheQueryBehavior<EntityPagedQuery<TReadModel>, EntityPagedResult<TReadModel>>>();
        services.AddTransient<IPipelineBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>, HybridCacheQueryBehavior<EntitySelectQuery<TReadModel>, IReadOnlyCollection<TReadModel>>>();

        services.AddTransient<IPipelineBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>, HybridCacheExpireBehavior<EntityCreateCommand<TCreateModel, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, HybridCacheExpireBehavior<EntityUpdateCommand<TKey, TUpdateModel, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>, HybridCacheExpireBehavior<EntityUpsertCommand<TKey, TUpdateModel, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityPatchCommand<TKey, TReadModel>, TReadModel>, HybridCacheExpireBehavior<EntityPatchCommand<TKey, TReadModel>, TReadModel>>();
        services.AddTransient<IPipelineBehavior<EntityDeleteCommand<TKey, TReadModel>, TReadModel>, HybridCacheExpireBehavior<EntityDeleteCommand<TKey, TReadModel>, TReadModel>>();

        return services;
    }
}
