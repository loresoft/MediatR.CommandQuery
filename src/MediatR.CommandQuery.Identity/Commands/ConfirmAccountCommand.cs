using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record ConfirmAccountCommand : IRequest<CompleteModel>
{
    public ConfirmAccountCommand(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);
        Token = token;
    }

    public string Token { get; }
}
