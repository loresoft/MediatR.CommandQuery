using System.Net.Http.Json;
using System.Text.Json;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.Models;

using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Dispatcher;

public class RemoteDispatcher : IDispatcher
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly DispatcherOptions _dispatcherOptions;
    private readonly HybridCache _hybridCache;

    public RemoteDispatcher(HttpClient httpClient, JsonSerializerOptions serializerOptions, IOptions<DispatcherOptions> dispatcherOptions, HybridCache hybridCache)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(serializerOptions);
        ArgumentNullException.ThrowIfNull(dispatcherOptions);
        ArgumentNullException.ThrowIfNull(hybridCache);

        _httpClient = httpClient;
        _serializerOptions = serializerOptions;
        _dispatcherOptions = dispatcherOptions.Value;
        _hybridCache = hybridCache;
    }

    public async Task<TResponse?> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // cache only if implements interface
        var cacheRequest = request as ICacheResult;
        if (cacheRequest?.IsCacheable() != true)
            return await SendCore(request, cancellationToken);

        var cacheKey = cacheRequest.GetCacheKey();
        var cacheTag = cacheRequest.GetCacheTag();
        var cacheOptions = new HybridCacheEntryOptions
        {
            Expiration = cacheRequest.SlidingExpiration()
        };

        return await _hybridCache.GetOrCreateAsync(
            key: cacheKey,
            factory: async token => await SendCore(request, token),
            options: cacheOptions,
            tags: string.IsNullOrEmpty(cacheTag) ? null : [cacheTag],
            cancellationToken: cancellationToken);
    }

    private async Task<TResponse?> SendCore<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
    {
        var requestUri = Combine(_dispatcherOptions.RoutePrefix, _dispatcherOptions.SendRoute);

        var dispatchRequest = new DispatchRequest { Request = request };

        var responseMessage = await _httpClient.PostAsJsonAsync(
            requestUri: requestUri,
            value: dispatchRequest,
            options: _serializerOptions,
            cancellationToken: cancellationToken);

        await EnsureSuccessStatusCode(responseMessage, cancellationToken);

        var response = await responseMessage.Content.ReadFromJsonAsync<TResponse>(
            options: _serializerOptions,
            cancellationToken: cancellationToken);

        // expire cache 
        if (request is not ICacheExpire cacheRequest)
            return response;

        var cacheTag = cacheRequest.GetCacheTag();
        if (!string.IsNullOrEmpty(cacheTag))
            await _hybridCache.RemoveByTagAsync(cacheTag, cancellationToken);

        return response;
    }

    private async Task EnsureSuccessStatusCode(HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
    {
        if (responseMessage.IsSuccessStatusCode)
            return;

        var message = $"Response status code does not indicate success: {responseMessage.StatusCode} ({responseMessage.ReasonPhrase}).";

        var mediaType = responseMessage.Content.Headers.ContentType?.MediaType;
        if (!string.Equals(mediaType, "application/problem+json", StringComparison.OrdinalIgnoreCase))
            throw new HttpRequestException(message, inner: null, responseMessage.StatusCode);

        var problemDetails = await responseMessage.Content.ReadFromJsonAsync<ProblemDetails>(
            options: _serializerOptions,
            cancellationToken: cancellationToken);

        if (problemDetails == null)
            throw new HttpRequestException(message, inner: null, responseMessage.StatusCode);

        var status = (System.Net.HttpStatusCode?)problemDetails.Status;
        status ??= responseMessage.StatusCode;

        var problemMessage = problemDetails.Title
            ?? responseMessage.ReasonPhrase
            ?? "Internal Server Error";

        if (!string.IsNullOrEmpty(problemDetails.Detail))
            problemMessage = $"{problemMessage} {problemDetails.Detail}";

        throw new HttpRequestException(
            message: problemMessage,
            inner: null,
            statusCode: status);
    }

    private static string Combine(string first, string second)
    {
        if (string.IsNullOrEmpty(first))
            return second;

        if (string.IsNullOrEmpty(second))
            return first;

        bool hasSeparator = first[^1] == '/' || second[0] == '/';

        return hasSeparator
            ? string.Concat(first, second)
            : $"{first}/{second}";
    }
}
