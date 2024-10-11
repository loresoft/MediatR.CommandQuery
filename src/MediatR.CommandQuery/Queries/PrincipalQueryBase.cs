using System.Security.Claims;

using MediatR.CommandQuery.Commands;

namespace MediatR.CommandQuery.Queries;

public abstract record PrincipalQueryBase<TResponse> : PrincipalCommandBase<TResponse>
{
    protected PrincipalQueryBase(ClaimsPrincipal? principal) : base(principal)
    {
    }
}
