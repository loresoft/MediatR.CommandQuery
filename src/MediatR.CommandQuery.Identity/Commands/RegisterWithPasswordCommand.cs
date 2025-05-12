using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record RegisterWithPasswordCommand : IRequest<CompleteModel>
{
    public RegisterWithPasswordCommand(RegisterWithPasswordModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Model = model;
    }

    public RegisterWithPasswordModel Model { get; }
}
