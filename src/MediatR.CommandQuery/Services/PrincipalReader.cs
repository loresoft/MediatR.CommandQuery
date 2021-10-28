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

            _logger.LogTrace("Resolved principal email: {email}", email);

            return email;
        }

        public string GetIdentifier(IPrincipal principal)
        {
            var name = principal?.Identity?.Name;

            _logger.LogTrace("Resolved principal identifier: {name}", name);

            return name;
        }

        public string GetName(IPrincipal principal)
        {
            var name = principal?.Identity?.Name;

            _logger.LogTrace("Resolved principal name: {name}", name);

            return name;
        }
    }
}
