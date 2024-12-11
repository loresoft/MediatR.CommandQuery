using Microsoft.AspNetCore.Builder;
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

    public static IEndpointConventionBuilder MapFeatureEndpoints(this IEndpointRouteBuilder builder, string prefix = "/api", string? serviceKey = null)
    {
        var featureGroup = builder.MapGroup(prefix);

        var features = string.IsNullOrEmpty(serviceKey)
            ? builder.ServiceProvider.GetServices<IFeatureEndpoint>()
            : builder.ServiceProvider.GetKeyedServices<IFeatureEndpoint>(serviceKey);

        foreach (var feature in features)
            feature.AddRoutes(featureGroup);

        return featureGroup;
    }
}
