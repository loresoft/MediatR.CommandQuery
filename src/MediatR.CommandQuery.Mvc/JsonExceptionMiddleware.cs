using System.Diagnostics;
using System.Net;
using System.Text.Json;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace MediatR.CommandQuery.Mvc;

public class JsonExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly Func<object, Task> _clearCacheHeadersDelegate;

    public JsonExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IWebHostEnvironment hostingEnvironment)
    {
        _next = next;
        _environment = hostingEnvironment;
        _logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        _clearCacheHeadersDelegate = ClearCacheHeaders;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            context.Response.StatusCode = 499; // Client Closed Request
            context.Response.OnStarting(_clearCacheHeadersDelegate, context.Response);

            return;
        }
        catch (Exception middlewareError)
        {
            _logger.LogError(middlewareError, "An unhandled exception has occurred while executing the request.");

            if (context.Response.HasStarted)
            {
                _logger.LogWarning("The response has already started, the error page middleware will not be executed.");
                throw;
            }

            try
            {
                // reset body
                if (context.Response.Body.CanSeek)
                    context.Response.Body.SetLength(0L);

                context.Response.StatusCode = 500;
                context.Response.OnStarting(_clearCacheHeadersDelegate, context.Response);

                await WriteContent(context, middlewareError, _environment.IsDevelopment()).ConfigureAwait(false);

                return;
            }
            catch (Exception handlerError)
            {
                // Suppress secondary exceptions, re-throw the original.
                _logger.LogError(handlerError, "An exception was thrown attempting to execute the error handler.");
            }

            throw; // Re-throw the original if we couldn't handle it
        }
    }

    private static async Task WriteContent(HttpContext context, Exception exception, bool includeDetails)
    {
        if (context is null)
            return;

        // ProblemDetails has it's own content type
        context.Response.ContentType = "application/problem+json";

        // Get the details to display, depending on whether we want to expose the raw exception
        var title = includeDetails ? "An error occured: " + exception.Message : "An error occured";
        var details = includeDetails ? exception.ToString() : null;

        var problem = new ProblemDetails
        {
            Status = 500,
            Title = title,
            Detail = details
        };

        // This is often very handy information for tracing the specific request
        var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
        if (traceId != null)
            problem.Extensions["traceId"] = traceId;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        switch (exception)
        {
            case ValidationException validationException:
                var errors = validationException.Errors
                    .Select(v => new { Name = v.PropertyName, Message = v.ErrorMessage, Value = v.AttemptedValue })
                    .ToList();

                context.Response.StatusCode = 422; // Unprocessable Entity
                problem.Extensions["errors"] = errors;
                break;

            case DomainException mediatorException:
                context.Response.StatusCode = mediatorException.StatusCode;
                break;
        }

        problem.Status = context.Response.StatusCode;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var options = ResolveSerializerOptions(context);

        await JsonSerializer.SerializeAsync(context.Response.Body, problem, options).ConfigureAwait(false);
    }

    private Task ClearCacheHeaders(object state)
    {
        if (state is not HttpResponse response)
            return Task.CompletedTask;

        response.Headers[HeaderNames.CacheControl] = "no-cache";
        response.Headers[HeaderNames.Pragma] = "no-cache";
        response.Headers[HeaderNames.Expires] = "-1";
        response.Headers.Remove(HeaderNames.ETag);

        return Task.CompletedTask;
    }

    private static JsonSerializerOptions ResolveSerializerOptions(HttpContext httpContext)
    {
        // Attempt to resolve options from DI then fallback to default options
        return httpContext.RequestServices?.GetService<IOptions<JsonOptions>>()?.Value?.JsonSerializerOptions ?? new JsonSerializerOptions();
    }
}
