using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record PasswordForgotSendCommand : IRequest<CompleteModel>
{
    public PasswordForgotSendCommand(PasswordForgotModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Model = model;
    }

    public PasswordForgotModel Model { get; }
}
