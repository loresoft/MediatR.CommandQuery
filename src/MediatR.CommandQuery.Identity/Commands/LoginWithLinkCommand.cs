using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record LoginWithLinkCommand : IRequest<CompleteModel>
{
    public LoginWithLinkCommand(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);
        Token = token;
    }

    public string Token { get; }
}
