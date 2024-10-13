using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.CommandQuery.Endpoints;

public static class FeatureEndpointExtensions
{
    public static IServiceCollection AddFeatureEndpoints(this IServiceCollection services)
    {
        services.Add(ServiceDescriptor.Transient<IFeatureEndpoint, DispatcherEndpoint>());

        return services;
    }

    public static IEndpointRouteBuilder MapFeatureEndpoints(this IEndpointRouteBuilder builder)
    {
        var features = builder.ServiceProvider.GetServices<IFeatureEndpoint>();
        foreach (var feature in features)
            feature.AddRoutes(builder);

        return builder;
    }
}
