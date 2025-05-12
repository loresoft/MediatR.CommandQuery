using System.Security.Claims;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Models;

namespace MediatR.CommandQuery.Identity.Commands;

public record LogoutSchemeCommand : PrincipalCommandBase<CompleteModel>
{
    public LogoutSchemeCommand(ClaimsPrincipal? principal, IReadOnlyCollection<string> schemes) : base(principal)
    {
        Schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
    }

    public IReadOnlyCollection<string> Schemes { get; }
}
