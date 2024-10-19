using FluentValidation;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Dispatcher;
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
}
