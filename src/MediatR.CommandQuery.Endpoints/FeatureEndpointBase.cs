using Microsoft.AspNetCore.Routing;

namespace MediatR.CommandQuery.Endpoints;

public abstract class FeatureEndpointBase : IFeatureEndpoint
{
    public abstract void AddRoutes(IEndpointRouteBuilder app);
}
