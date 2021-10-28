using System.Security.Principal;

namespace MediatR.CommandQuery.Definitions
{
    public interface IPrincipalReader
    {
        public string GetIdentifier(IPrincipal principal);

        public string GetName(IPrincipal principal);

        public string GetEmail(IPrincipal principal);
    }
}
