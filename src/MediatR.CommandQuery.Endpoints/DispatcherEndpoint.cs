using System.Security.Claims;

using MediatR.CommandQuery.Dispatcher;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Endpoints;

public class DispatcherEndpoint : IFeatureEndpoint
{
    private readonly ISender _sender;
    private readonly DispatcherOptions _dispatcherOptions;

    public DispatcherEndpoint(ISender sender, IOptions<DispatcherOptions> dispatcherOptions)
    {
        _sender = sender;
        _dispatcherOptions = dispatcherOptions.Value;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(_dispatcherOptions.DispatcherPrefix);

        group
            .MapPost(_dispatcherOptions.SendRoute, Send)
            .WithTags("Dispatcher")
            .WithName($"Send")
            .WithSummary("Send Mediator command")
            .WithDescription("Send Mediator command");
    }

    protected virtual async Task<IResult> Send(
        [FromBody] DispatchRequest dispatchRequest,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = dispatchRequest.Request;
            var result = await _sender.Send(request, cancellationToken);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            var details = ex.ToProblemDetails();
            return Results.Problem(details);
        }
    }
}
