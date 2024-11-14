using System.Security.Claims;

using MediatR.CommandQuery.Dispatcher;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Endpoints;

public class DispatcherEndpoint : IFeatureEndpoint
{
    private readonly ISender _sender;
    private readonly DispatcherOptions _dispatcherOptions;
    private readonly ILogger<DispatcherEndpoint> _logger;

    public DispatcherEndpoint(ILogger<DispatcherEndpoint> logger, ISender sender, IOptions<DispatcherOptions> dispatcherOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(dispatcherOptions);

        _sender = sender;
        _dispatcherOptions = dispatcherOptions.Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(_dispatcherOptions.DispatcherPrefix);

        group
            .MapPost(_dispatcherOptions.SendRoute, Send)
            .WithEntityMetadata("Dispatcher")
            .WithName($"Send")
            .WithSummary("Send Mediator command")
            .WithDescription("Send Mediator command");
    }

    protected virtual async Task<Results<Ok<object>, ProblemHttpResult>> Send(
        [FromBody] DispatchRequest dispatchRequest,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = dispatchRequest.Request;

            var result = await _sender.Send(request, cancellationToken).ConfigureAwait(false);
            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dispatching request: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }
    }
}
