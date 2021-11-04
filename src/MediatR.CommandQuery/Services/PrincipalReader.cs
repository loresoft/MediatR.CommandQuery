using System;
using System.Security.Claims;
using System.Security.Principal;

using MediatR.CommandQuery.Definitions;

using Microsoft.Extensions.Logging;

namespace MediatR.CommandQuery.Services
{
    public class PrincipalReader : IPrincipalReader
    {
        private readonly ILogger<PrincipalReader> _logger;

        public PrincipalReader(ILogger<PrincipalReader> logger)
        {
            _logger = logger;
        }

        public string GetEmail(IPrincipal principal)
        {
            var claimPrincipal = principal as ClaimsPrincipal;
            var emailClaim = claimPrincipal?.FindFirst(ClaimTypes.Email);

            var email = emailClaim?.Value;

            _logPrincipal(_logger, "Email", email, null);

            return email;
        }

        public string GetIdentifier(IPrincipal principal)
        {
            var name = principal?.Identity?.Name;

            _logPrincipal(_logger, "Identifier", name, null);

            return name;
        }

        public string GetName(IPrincipal principal)
        {
            var name = principal?.Identity?.Name;

            _logPrincipal(_logger, "Name", name, null);

            return name;
        }

        private static readonly Action<ILogger, string, string, Exception> _logPrincipal
            = LoggerMessage.Define<string, string>(LogLevel.Trace, 0, "Resolved principal claim {type}: {value}");

    }
}
