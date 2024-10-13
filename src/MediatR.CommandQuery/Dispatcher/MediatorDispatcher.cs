namespace MediatR.CommandQuery.Dispatcher;

public class MediatorDispatcher : IDispatcher
{
    private readonly ISender _sender;

    public MediatorDispatcher(ISender sender)
    {
        _sender = sender;
    }

    public async Task<TResponse?> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return await _sender.Send(request, cancellationToken);
    }
}
