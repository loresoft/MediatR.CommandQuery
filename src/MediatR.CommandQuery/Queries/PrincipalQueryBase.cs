using System.Security.Principal;

namespace MediatR.CommandQuery.Queries;

public abstract class PrincipalQueryBase<TResponse> : IRequest<TResponse>
{
    protected PrincipalQueryBase(IPrincipal? principal)
    {
        Principal = principal;
    }

    public IPrincipal? Principal { get; }
}
