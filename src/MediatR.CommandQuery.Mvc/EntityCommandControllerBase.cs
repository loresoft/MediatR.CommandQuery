using System.Net.Mime;

using MediatR.CommandQuery.Commands;

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


    [HttpPost("")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel>> Create(
        [FromBody] TCreateModel createModel,
        CancellationToken cancellationToken = default)
    {
        return await CreateCommand(createModel, cancellationToken);
    }

    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel>> Update(
        [FromRoute] TKey id,
        [FromBody] TUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        return await UpdateCommand(id, updateModel, cancellationToken);
    }

    [HttpPatch("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel>> Patch(
        [FromRoute] TKey id,
        [FromBody] JsonPatchDocument jsonPatch,
        CancellationToken cancellationToken = default)
    {
        return await PatchCommand(id, jsonPatch, cancellationToken);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel>> Delete(
        [FromRoute] TKey id,
        CancellationToken cancellationToken = default)
    {
        return await DeleteCommand(id, cancellationToken);
    }


    protected virtual async Task<TReadModel> CreateCommand(TCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var command = new EntityCreateCommand<TCreateModel, TReadModel>(User, createModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> UpdateCommand(TKey id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(User, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> UpsertCommand(TKey id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(User, id, updateModel);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> PatchCommand(TKey id, JsonPatchDocument jsonPatch, CancellationToken cancellationToken = default)
    {
        var command = new EntityPatchCommand<TKey, TReadModel>(User, id, jsonPatch);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<TReadModel> DeleteCommand(TKey id, CancellationToken cancellationToken = default)
    {
        var command = new EntityDeleteCommand<TKey, TReadModel>(User, id);
        return await Mediator.Send(command, cancellationToken);
    }
}
