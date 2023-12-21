using Hangfire;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Newtonsoft.Json;

namespace MediatR.CommandQuery.Hangfire;

public static class HangfireConfigurationExtensions
{
    public static IGlobalConfiguration UseMediatR(this IGlobalConfiguration configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        configuration.UseSerializerSettings(jsonSettings);

        return configuration;
    }

    public static IServiceCollection AddMediatorDispatcher(this IServiceCollection services)
    {
        if (services is null)
            throw new System.ArgumentNullException(nameof(services));

        services.TryAddScoped<IMediatorDispatcher, MediatorDispatcher>();

        return services;
    }
}
