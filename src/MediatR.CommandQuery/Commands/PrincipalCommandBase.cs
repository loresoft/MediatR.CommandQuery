using System;
using System.Security.Principal;

namespace MediatR.CommandQuery.Commands;

public abstract class PrincipalCommandBase<TResponse> : IRequest<TResponse>
{
    protected PrincipalCommandBase(IPrincipal? principal)
    {
        Principal = principal;
        ActivatedBy = principal?.Identity?.Name;
        Activated = DateTimeOffset.UtcNow;
    }

    public IPrincipal? Principal { get; }

    public DateTimeOffset Activated { get; }

    public string? ActivatedBy { get; }

    public override string ToString()
    {
        return $"Date: {Activated}; User: {ActivatedBy}";
    }

}
