using System.Security.Claims;
using System.Security.Principal;

using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Services;

public partial class PrincipalReader : IPrincipalReader
{
    private readonly ILogger<PrincipalReader> _logger;

    public PrincipalReader(ILogger<PrincipalReader> logger)
    {
        _logger = logger;
    }

    public string? GetEmail(IPrincipal? principal)
    {
        var claimPrincipal = principal as ClaimsPrincipal;
        var emailClaim = claimPrincipal?.FindFirst(ClaimTypes.Email);

        var email = emailClaim?.Value;

        LogPrincipal(_logger, "Email", email);

        return email;
    }

    public string? GetIdentifier(IPrincipal? principal)
    {
        var name = principal?.Identity?.Name;

        LogPrincipal(_logger, "Identifier", name);

        return name;
    }

    public string? GetName(IPrincipal? principal)
    {
        var name = principal?.Identity?.Name;

        LogPrincipal(_logger, "Name", name);

        return name;
    }

    [LoggerMessage(1, LogLevel.Trace, "Resolved principal claim {Type}: {Value}")]
    static partial void LogPrincipal(ILogger logger, string type, string? value);
}
