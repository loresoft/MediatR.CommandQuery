using System.Security.Claims;
using System.Text.Json.Serialization;

using MediatR.CommandQuery.Converters;

namespace MediatR.CommandQuery.Commands;

public abstract record PrincipalCommandBase<TResponse> : IRequest<TResponse>
{
    protected PrincipalCommandBase(ClaimsPrincipal? principal)
    {
        Principal = principal;

        Activated = DateTimeOffset.UtcNow;
        ActivatedBy = principal?.Identity?.Name ?? "system";
    }

    [JsonConverter(typeof(ClaimsPrincipalConverter))]
    public ClaimsPrincipal? Principal { get; }

    public DateTimeOffset Activated { get; }

    public string? ActivatedBy { get; }
}
