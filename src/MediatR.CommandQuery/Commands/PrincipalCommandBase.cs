using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Results;

namespace MediatR.CommandQuery.Commands;

public abstract class PrincipalCommandBase<TResponse> : IRequest<IResult<TResponse>>
{
    protected PrincipalCommandBase(IPrincipal? principal)
        : this(DateTimeOffset.UtcNow, principal?.Identity?.Name)
    {
        Principal = principal;
    }

    protected PrincipalCommandBase(DateTimeOffset activated, string? activatedBy)
    {
        Activated = activated;
        ActivatedBy = activatedBy;
    }

    [JsonIgnore]
    [IgnoreDataMember]
    public IPrincipal? Principal { get; }

    public DateTimeOffset Activated { get; }

    public string? ActivatedBy { get; }

    public override string ToString()
    {
        return $"Activated: {Activated}; ActivatedBy: {ActivatedBy}";
    }

    // ignore Principal property without attribute for JSON.NET
    public bool ShouldSerializePrincipal() => false;
}
