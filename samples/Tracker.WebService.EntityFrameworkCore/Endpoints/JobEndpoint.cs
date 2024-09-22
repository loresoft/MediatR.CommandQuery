using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using MediatR.CommandQuery.Endpoints;
using MediatR.CommandQuery.Hangfire;
using MediatR.CommandQuery.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Tracker.WebService.Domain.Commands;

namespace Tracker.WebService.Endpoints;

[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]
public class JobEndpoint : MediatorEndpointBase
{
    public JobEndpoint(IMediator mediator) : base(mediator)
    {
        EntityName = "Jobs";
        RoutePrefix = $"/api/{EntityName}";
    }

    public string EntityName { get; }

    public string RoutePrefix { get; }


    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RoutePrefix);

        MapGroup(group);
    }

    protected virtual void MapGroup(RouteGroupBuilder group)
    {
        group
            .MapPost("{id}", RunJob)
            .WithTags(EntityName)
            .WithName($"RunJob")
            .WithSummary("Run job id")
            .WithDescription("Run job id");
    }

    private async Task RunJob(
        [FromRoute] int id,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new BackgroundUpdateCommand(id);
        await Mediator.Enqueue(command, cancellationToken);
    }
}
