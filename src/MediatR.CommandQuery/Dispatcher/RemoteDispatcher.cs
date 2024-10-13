using System.Net.Http.Json;
using System.Text.Json;

using MediatR.CommandQuery.Models;

using Microsoft.Extensions.Options;

namespace MediatR.CommandQuery.Dispatcher;

public class RemoteDispatcher : IDispatcher
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly DispatcherOptions _dispatcherOptions;

    public RemoteDispatcher(HttpClient httpClient, JsonSerializerOptions serializerOptions, IOptions<DispatcherOptions> dispatcherOptions)
    {
        _httpClient = httpClient;
        _serializerOptions = serializerOptions;
        _dispatcherOptions = dispatcherOptions.Value;
    }

    public async Task<TResponse?> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestUri = Combine(_dispatcherOptions.RoutePrefix, _dispatcherOptions.SendRoute);

        var dispatchRequest = new DispatchRequest { Request = request };

        var responseMessage = await _httpClient.PostAsJsonAsync(
            requestUri: requestUri,
            value: dispatchRequest,
            options: _serializerOptions,
            cancellationToken: cancellationToken);

        await EnsureSuccessStatusCode(responseMessage, cancellationToken);

        return await responseMessage.Content.ReadFromJsonAsync<TResponse>(
            options: _serializerOptions,
            cancellationToken: cancellationToken);
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
