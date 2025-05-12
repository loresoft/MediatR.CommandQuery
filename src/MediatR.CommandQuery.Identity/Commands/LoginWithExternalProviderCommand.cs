using MediatR.CommandQuery.Identity.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record LoginWithExternalProviderCommand : IRequest<LoginCompleteModel>
{
    public LoginWithExternalProviderCommand(string? action, string? remoteError)
    {
        Action = action;
        RemoteError = remoteError;
    }

    public string? Action { get; }

    public string? RemoteError { get; }
}
