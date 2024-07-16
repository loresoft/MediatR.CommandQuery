using System.Security.Claims;

using MediatR.CommandQuery.Queries;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MediatR.CommandQuery.Endpoints;

public abstract class EntityQueryEndpointBase<TKey, TListModel, TReadModel>
    : MediatorEndpointBase
{
    protected EntityQueryEndpointBase(IMediator mediator, string entityName) : base(mediator)
    {
        EntityName = entityName;
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
            .MapGet("{id}", GetQuery)
            .Produces<TReadModel>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Get{EntityName}")
            .WithSummary("Get an entity by id")
            .WithDescription("Get an entity by id");

        group
            .MapGet("page", GetPagedQuery)
            .Produces<EntityPagedResult<TListModel>>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Get{EntityName}Page")
            .WithSummary("Get a page of entities")
            .WithDescription("Get a page of entities");

        group
            .MapPost("page", PostPagedQuery)
            .Produces<EntityPagedResult<TListModel>>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Query{EntityName}Page")
            .WithSummary("Get a page of entities")
            .WithDescription("Get a page of entities");

        group
            .MapGet("", GetSelectQuery)
            .Produces<IReadOnlyCollection<TListModel>>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Get{EntityName}List")
            .WithSummary("Get entities by query")
            .WithDescription("Get entities by query");

        group
            .MapPost("query", PostSelectQuery)
            .Produces<IReadOnlyCollection<TListModel>>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(EntityName)
            .WithName($"Query{EntityName}List")
            .WithSummary("Get entities by query")
            .WithDescription("Get entities by query");
    }


    protected virtual async Task<TReadModel?> GetQuery(
        [FromRoute] TKey id,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityIdentifierQuery<TKey, TReadModel>(user, id);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<EntityPagedResult<TListModel>> GetPagedQuery(
        [FromQuery] string? q = null,
        [FromQuery] string? sort = null,
        [FromQuery] int? page = 1,
        [FromQuery] int? size = 20,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var entityQuery = new EntityQuery(q, page ?? 1, size ?? 20, sort);
        var command = new EntityPagedQuery<TListModel>(user, entityQuery);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<EntityPagedResult<TListModel>> PostPagedQuery(
        [FromBody] EntityQuery entityQuery,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntityPagedQuery<TListModel>(user, entityQuery);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<IReadOnlyCollection<TListModel>> GetSelectQuery(
        [FromQuery] string? q = null,
        [FromQuery] string? sort = null,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var entitySelect = new EntitySelect(q, sort);
        var command = new EntitySelectQuery<TListModel>(user, entitySelect);
        return await Mediator.Send(command, cancellationToken);
    }

    protected virtual async Task<IReadOnlyCollection<TListModel>> PostSelectQuery(
        [FromBody] EntitySelect entitySelect,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        var command = new EntitySelectQuery<TListModel>(user, entitySelect);
        return await Mediator.Send(command, cancellationToken);
    }
}
