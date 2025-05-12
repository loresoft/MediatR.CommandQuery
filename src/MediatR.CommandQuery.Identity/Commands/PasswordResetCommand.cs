using MediatR.CommandQuery.Identity.Models;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record PasswordResetCommand : IRequest<CompleteModel>
{
    public PasswordResetCommand(PasswordResetModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Model = model;
    }

    public PasswordResetModel Model { get; }
}
