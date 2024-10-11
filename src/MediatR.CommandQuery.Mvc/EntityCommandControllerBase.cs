// Ignore Spelling: Upsert json

using System.Net.Mime;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Queries;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SystemTextJsonPatch;

namespace MediatR.CommandQuery.Mvc;

[Produces(MediaTypeNames.Application.Json)]
public abstract class EntityCommandControllerBase<TKey, TListModel, TReadModel, TCreateModel, TUpdateModel>
    : EntityQueryControllerBase<TKey, TListModel, TReadModel>
{
    protected EntityCommandControllerBase(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}/update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TUpdateModel?>> GetUpdate(
        [FromRoute] TKey id,
        CancellationToken cancellationToken = default)
    {
        var result = await GetUpdateQuery(id, cancellationToken);
        return Result(result);
    }

    [HttpPost("")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel?>> Create(
        [FromBody] TCreateModel createModel,
        CancellationToken cancellationToken = default)
    {
        var result = await CreateCommand(createModel, cancellationToken);
        return Result(result);
    }

    [HttpPost("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel?>> Upsert(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        var result = await UpsertCommand(id, updateModel, cancellationToken);
        return Result(result);
    }

    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel?>> Update(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        var result = await UpdateCommand(id, updateModel, cancellationToken);
        return Result(result);
    }

    [HttpPatch("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel?>> Patch(
        [FromRoute] TKey id,
        [FromBody] JsonPatchDocument jsonPatch,
        CancellationToken cancellationToken = default)
    {
        var result = await PatchCommand(id, jsonPatch, cancellationToken);
        return Result(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel?>> Delete(
        [FromRoute] TKey id,
        CancellationToken cancellationToken = default)
    {
        var result = await DeleteCommand(id, cancellationToken);
        return Result(result);
    }

    protected virtual async Task<Results.IResult<TUpdateModel?>> GetUpdateQuery(TKey id, CancellationToken cancellationToken = default)
    {
        var command = new EntityIdentifierQuery<TKey, TUpdateModel>(User, id);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<Results.IResult<TReadModel?>> CreateCommand(TCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var command = new EntityCreateCommand<TCreateModel, TReadModel>(User, createModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<Results.IResult<TReadModel?>> UpdateCommand(TKey id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(User, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<Results.IResult<TReadModel?>> UpsertCommand(TKey id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(User, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<Results.IResult<TReadModel?>> PatchCommand(TKey id, JsonPatchDocument jsonPatch, CancellationToken cancellationToken = default)
    {
        var command = new EntityPatchCommand<TKey, TReadModel>(User, id, jsonPatch);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<Results.IResult<TReadModel?>> DeleteCommand(TKey id, CancellationToken cancellationToken = default)
    {
        var command = new EntityDeleteCommand<TKey, TReadModel>(User, id);
        return await Mediator.Send(command, cancellationToken);
    }
}
