using System.Security.Claims;

using MediatR.CommandQuery.Commands;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MediatR.CommandQuery.Endpoints;

public abstract class EntityCommandEndpointBase<TKey, TReadModel, TCreateModel, TUpdateModel>
    : IFeatureEndpoint
{
    protected EntityCommandEndpointBase(IMediator mediator, string entityName)
    {
        Mediator = mediator;
        EntityName = entityName;
        RoutePrefix = $"/api/{EntityName}";
    }

    public IMediator Mediator { get; }


    public string EntityName { get; }

    public string RoutePrefix { get; }


    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RoutePrefix);

        MapGroup(group);
    }


    protected virtual void MapGroup(RouteGroupBuilder group)
    {
        group
            .MapPost("", CreateCommand)
            .Produces<TReadModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Create{EntityName}")
            .WithSummary("Create new entity")
            .WithDescription("Create new entity");

        group
            .MapPost("{id}", UpsertCommand)
            .Produces<TReadModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Upsert{EntityName}")
            .WithSummary("Create new or update entity")
            .WithDescription("Create new or update entity");

        group
            .MapPut("{id}", UpdateCommand)
            .Produces<TReadModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Update{EntityName}")
            .WithSummary("Update entity")
            .WithDescription("Update entity");

        group
            .MapPatch("{id}", PatchCommand)
            .Produces<TReadModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Patch{EntityName}")
            .WithSummary("Patch entity")
            .WithDescription("Patch entity");

        group
            .MapDelete("{id}", DeleteCommand)
            .Produces<TReadModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Delete{EntityName}")
            .WithSummary("Delete entity")
            .WithDescription("Delete entity");
    }


    protected virtual async Task<TReadModel> CreateCommand(
        CancellationToken cancellationToken,
        ClaimsPrincipal user,
        [FromBody] TCreateModel createModel)
    {
        var command = new EntityCreateCommand<TCreateModel, TReadModel>(user, createModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> UpdateCommand(
        CancellationToken cancellationToken,
        ClaimsPrincipal user,
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel)
    {
        var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> UpsertCommand(
        CancellationToken cancellationToken,
        ClaimsPrincipal user,
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel)
    {
        var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> PatchCommand(
        CancellationToken cancellationToken,
        ClaimsPrincipal user,
        [FromRoute] TKey id,
        [FromBody] IJsonPatchDocument jsonPatch)
    {
        var command = new EntityPatchCommand<TKey, TReadModel>(user, id, jsonPatch);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> DeleteCommand(
        CancellationToken cancellationToken,
        ClaimsPrincipal user,
        [FromRoute] TKey id)
    {
        var command = new EntityDeleteCommand<TKey, TReadModel>(user, id);
        return await Mediator.Send(command, cancellationToken);
    }

}