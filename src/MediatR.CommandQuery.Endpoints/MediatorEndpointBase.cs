using Microsoft.AspNetCore.Routing;

namespace MediatR.CommandQuery.Endpoints;

public abstract class MediatorEndpointBase : IFeatureEndpoint
{
    protected MediatorEndpointBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    public IMediator Mediator { get; }

    public abstract void AddRoutes(IEndpointRouteBuilder app);
}
