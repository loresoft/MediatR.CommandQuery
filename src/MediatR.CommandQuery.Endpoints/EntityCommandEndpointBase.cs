using System.Security.Claims;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Queries;

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

#pragma warning disable MA0051 // Method is too long
    protected override void MapGroup(RouteGroupBuilder group)
#pragma warning restore MA0051 // Method is too long
    {
        base.MapGroup(group);

        group
            .MapGet("{id}/update", GetUpdateQuery)
            .Produces<TUpdateModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Get{EntityName}Update")
            .WithSummary("Get an entity for update by id")
            .WithDescription("Get an entity for update by id");

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

    protected virtual async Task<TUpdateModel?> GetUpdateQuery(
        [FromRoute] TKey id,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityIdentifierQuery<TKey, TUpdateModel>(user, id);
        return await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }

    protected virtual async Task<TReadModel?> CreateCommand(
        [FromBody] TCreateModel createModel,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityCreateCommand<TCreateModel, TReadModel>(user, createModel);
        return await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }

    protected virtual async Task<TReadModel?> UpdateCommand(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);
        return await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }

    protected virtual async Task<TReadModel?> UpsertCommand(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(user, id, updateModel);
        return await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }

    protected virtual async Task<TReadModel?> PatchCommand(
        [FromRoute] TKey id,
        [FromBody] JsonPatchDocument jsonPatch,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityPatchCommand<TKey, TReadModel>(user, id, jsonPatch);
        return await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }

    protected virtual async Task<TReadModel?> DeleteCommand(
        [FromRoute] TKey id,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityDeleteCommand<TKey, TReadModel>(user, id);
        return await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
    }

}
