using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

    protected virtual Results<ProblemHttpResult, Ok<T>> Result<T>(Results.IResult<T> result)
    {
        if (result.IsSuccess)
            return TypedResults.Ok(result.Value);

        var problemDetails = result.Error.Problem();
        return TypedResults.Problem(problemDetails);
    }
}
