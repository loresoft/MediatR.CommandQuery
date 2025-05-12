using MediatR.CommandQuery.Identity.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record LoginWithPasswordCommand : IRequest<LoginCompleteModel>
{
    public LoginWithPasswordCommand(LoginWithPasswordModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Model = model;
    }

    public LoginWithPasswordModel Model { get; }
}
