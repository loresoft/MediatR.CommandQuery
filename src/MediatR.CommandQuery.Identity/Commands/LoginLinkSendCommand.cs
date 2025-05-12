using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record LoginLinkSendCommand : IRequest<CompleteModel>
{
    public LoginLinkSendCommand(LoginLinkSendModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Model = model;
    }

    public LoginLinkSendModel Model { get; }
}
