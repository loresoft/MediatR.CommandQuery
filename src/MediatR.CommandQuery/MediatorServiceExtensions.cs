using FluentValidation;

using MediatR.NotificationPublishers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.CommandQuery;

public static class MediatorServiceExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.TryAdd(new ServiceDescriptor(typeof(IMediator), typeof(Mediator), lifetime));
        services.TryAdd(new ServiceDescriptor(typeof(ISender), (sp) => sp.GetRequiredService<IMediator>(), lifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IPublisher), (sp) => sp.GetRequiredService<IMediator>(), lifetime));
        services.TryAdd(new ServiceDescriptor(typeof(INotificationPublisher), typeof(TaskWhenAllPublisher), lifetime));

        return services;
    }

    public static IServiceCollection AddValidatorsFromAssembly<T>(this IServiceCollection services)
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

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
}
