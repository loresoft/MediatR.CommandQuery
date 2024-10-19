using System.Security.Principal;

namespace MediatR.CommandQuery.Definitions;

public interface IPrincipalReader
{
    string? GetIdentifier(IPrincipal? principal);

    string? GetName(IPrincipal? principal);

    string? GetEmail(IPrincipal? principal);

    Guid? GetObjectId(IPrincipal? principal);
}
