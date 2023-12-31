using Microsoft.AspNetCore.Routing;

namespace MediatR.CommandQuery.Endpoints;

public interface IFeatureEndpoint
{
    void AddRoutes(IEndpointRouteBuilder app);
}
