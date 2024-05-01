using System.Security.Claims;

using MediatR.CommandQuery.Commands;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using SystemTextJsonPatch;

namespace MediatR.CommandQuery.Endpoints;

public abstract class EntityCommandEndpointBase<TKey, TListModel, TReadModel, TCreateModel, TUpdateModel>
    : EntityQueryEndpointBase<TKey, TListModel, TReadModel>
{
    protected EntityCommandEndpointBase(IMediator mediator, string entityName) : base(mediator, entityName)
    {
    }

    protected override void MapGroup(RouteGroupBuilder group)
    {
        base.MapGroup(group);

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
        [FromBody] TCreateModel createModel,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityCreateCommand<TCreateModel, TReadModel>(user, createModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> UpdateCommand(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> UpsertCommand(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> PatchCommand(
        [FromRoute] TKey id,
        [FromBody] JsonPatchDocument jsonPatch,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityPatchCommand<TKey, TReadModel>(user, id, jsonPatch);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> DeleteCommand(
        [FromRoute] TKey id,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityDeleteCommand<TKey, TReadModel>(user, id);
        return await Mediator.Send(command, cancellationToken);
    }

}
