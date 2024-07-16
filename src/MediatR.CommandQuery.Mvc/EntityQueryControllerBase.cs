using System.Net.Mime;

using MediatR.CommandQuery.Queries;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc;

[Produces(MediaTypeNames.Application.Json)]
public abstract class EntityQueryControllerBase<TKey, TListModel, TReadModel> : MediatorControllerBase
{
    protected EntityQueryControllerBase(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<TReadModel?>> Get(
        [FromRoute] TKey id,
        CancellationToken cancellationToken = default)
    {
        return await GetQuery(id, cancellationToken);
    }

    [HttpPost("page")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<EntityPagedResult<TListModel>>> Page(
        [FromBody] EntityQuery query,
        CancellationToken cancellationToken = default)
    {
        return await PagedQuery(query, cancellationToken);
    }

    [HttpGet("page")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<EntityPagedResult<TListModel>>> Page(
        [FromQuery] string? q = null,
        [FromQuery] string? sort = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new EntityQuery(q, page, size, sort);
        return await PagedQuery(query, cancellationToken);
    }

    [HttpPost("query")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<IReadOnlyCollection<TListModel>>> Query(
        [FromBody] EntitySelect query,
        CancellationToken cancellationToken = default)
    {
        var results = await SelectQuery(query, cancellationToken);

        return results.ToList();
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public virtual async Task<ActionResult<IReadOnlyCollection<TListModel>>> Query(
        [FromQuery] string? q = null,
        [FromQuery] string? sort = null,
        CancellationToken cancellationToken = default)
    {
        var query = new EntitySelect(q, sort);
        var results = await SelectQuery(query, cancellationToken);

        return results.ToList();
    }


    protected virtual async Task<TReadModel?> GetQuery(TKey id, CancellationToken cancellationToken = default)
    {
        var command = new EntityIdentifierQuery<TKey, TReadModel>(User, id);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<EntityPagedResult<TListModel>> PagedQuery(EntityQuery entityQuery, CancellationToken cancellationToken = default)
    {
        var command = new EntityPagedQuery<TListModel>(User, entityQuery);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<IReadOnlyCollection<TListModel>> SelectQuery(EntitySelect entitySelect, CancellationToken cancellationToken = default)
    {
        var command = new EntitySelectQuery<TListModel>(User, entitySelect);
        return await Mediator.Send(command, cancellationToken);
    }
}
