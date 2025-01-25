using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

using CsvHelper;
using CsvHelper.Configuration;

using MediatR.CommandQuery.Queries;
using MediatR.CommandQuery.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Endpoints;

public abstract class EntityQueryEndpointBase<TKey, TListModel, TReadModel> : FeatureEndpointBase
{
    protected EntityQueryEndpointBase(ILoggerFactory loggerFactory, string entityName, string? routePrefix = null)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory);
        ArgumentException.ThrowIfNullOrEmpty(entityName);

        Logger = loggerFactory.CreateLogger(GetType());

        EntityName = entityName;
        RoutePrefix = routePrefix ?? EntityName;
    }

    public string EntityName { get; }

    public string RoutePrefix { get; }


    protected ILogger Logger { get; }


    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RoutePrefix);

        MapGroup(group);
    }


    protected virtual void MapGroup(RouteGroupBuilder group)
    {
        group
            .MapGet("{id}", GetQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"Get{EntityName}")
            .WithSummary("Get an entity by id")
            .WithDescription("Get an entity by id");

        group
            .MapGet("page", GetPagedQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"Get{EntityName}Page")
            .WithSummary("Get a page of entities")
            .WithDescription("Get a page of entities");

        group
            .MapPost("page", PostPagedQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"Query{EntityName}Page")
            .WithSummary("Get a page of entities")
            .WithDescription("Get a page of entities");

        group
            .MapGet("", GetSelectQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"Get{EntityName}List")
            .WithSummary("Get entities by query")
            .WithDescription("Get entities by query");

        group
            .MapPost("query", PostSelectQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"Query{EntityName}List")
            .WithSummary("Get entities by query")
            .WithDescription("Get entities by query");

        group
            .MapPost("export", PostExportQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"Export{EntityName}List")
            .WithSummary("Export entities by query")
            .WithDescription("Export entities by query");

        group
            .MapGet("export", GetExportQuery)
            .WithEntityMetadata(EntityName)
            .WithName($"GetExport{EntityName}List")
            .WithSummary("Get Export entities by query")
            .WithDescription("Get Export entities by query");
    }


    protected virtual async Task<Results<Ok<TReadModel>, ProblemHttpResult>> GetQuery(
        [FromServices] ISender sender,
        [FromRoute] TKey id,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new EntityIdentifierQuery<TKey, TReadModel>(user, id);

            var result = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error GetQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }
    }

    protected virtual async Task<Results<Ok<EntityPagedResult<TListModel>>, ProblemHttpResult>> GetPagedQuery(
        [FromServices] ISender sender,
        [FromQuery] string? q = null,
        [FromQuery] string? sort = null,
        [FromQuery] int? page = 1,
        [FromQuery] int? size = 20,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entityQuery = new EntityQuery(q, page ?? 1, size ?? 20, sort);
            var command = new EntityPagedQuery<TListModel>(user, entityQuery);

            var result = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error GetPagedQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }

    }

    protected virtual async Task<Results<Ok<EntityPagedResult<TListModel>>, ProblemHttpResult>> PostPagedQuery(
        [FromServices] ISender sender,
        [FromBody] EntityQuery entityQuery,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new EntityPagedQuery<TListModel>(user, entityQuery);

            var result = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error PostPagedQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }

    }

    protected virtual async Task<Results<Ok<IReadOnlyCollection<TListModel>>, ProblemHttpResult>> GetSelectQuery(
        [FromServices] ISender sender,
        [FromQuery] string? q = null,
        [FromQuery] string? sort = null,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entitySelect = new EntitySelect(q, sort);

            var command = new EntitySelectQuery<TListModel>(user, entitySelect);

            var result = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error GetSelectQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }

    }

    protected virtual async Task<Results<Ok<IReadOnlyCollection<TListModel>>, ProblemHttpResult>> PostSelectQuery(
        [FromServices] ISender sender,
        [FromBody] EntitySelect entitySelect,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new EntitySelectQuery<TListModel>(user, entitySelect);

            var result = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error PostSelectQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }

    }

    protected virtual async Task<Results<FileContentHttpResult, ProblemHttpResult>> PostExportQuery(
        [FromServices] ISender sender,
        [FromBody] EntitySelect entitySelect,
        [FromServices] CsvConfiguration? csvConfiguration = default,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new EntitySelectQuery<TListModel>(user, entitySelect);
            var results = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            csvConfiguration ??= new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            var buffer = await ConvertToCsv(results, csvConfiguration, cancellationToken).ConfigureAwait(false);

            return TypedResults.File(buffer, "text/csv");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error PostExportQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }

    }

    protected virtual async Task<Results<FileContentHttpResult, ProblemHttpResult>> GetExportQuery(
        [FromServices] ISender sender,
        [FromQuery] string? encodedQuery = null,
        [FromServices] CsvConfiguration? csvConfiguration = default,
        [FromServices] JsonSerializerOptions? jsonSerializerOptions = default,
        ClaimsPrincipal? user = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            jsonSerializerOptions ??= new JsonSerializerOptions(JsonSerializerDefaults.Web);

            var entitySelect = QueryStringEncoder.Decode<EntitySelect>(encodedQuery, jsonSerializerOptions) ?? new EntitySelect();
            var command = new EntitySelectQuery<TListModel>(user, entitySelect);
            var results = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            csvConfiguration ??= new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            var buffer = await ConvertToCsv(results, csvConfiguration, cancellationToken).ConfigureAwait(false);

            return TypedResults.File(buffer, "text/csv");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error GetExportQuery: {ErrorMessage}", ex.Message);

            var details = ex.ToProblemDetails();
            return TypedResults.Problem(details);
        }
    }


    protected virtual void WriteExportData(CsvWriter csvWriter, IReadOnlyCollection<TListModel> results)
    {
        csvWriter.WriteRecords(results);
    }

    private async Task<byte[]> ConvertToCsv(IReadOnlyCollection<TListModel> results, CsvConfiguration? csvConfiguration, CancellationToken cancellationToken = default)
    {
        csvConfiguration ??= new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

#pragma warning disable MA0004 // Use Task.ConfigureAwait
        await using var memoryStream = new MemoryStream();
        await using var streamWriter = new StreamWriter(memoryStream);
        await using var csvWriter = new CsvWriter(streamWriter, csvConfiguration);
#pragma warning restore MA0004 // Use Task.ConfigureAwait

        WriteExportData(csvWriter, results);

        await streamWriter.FlushAsync(cancellationToken).ConfigureAwait(false);

        return memoryStream.ToArray();
    }
}
