using System.Security.Principal;
using System.Threading.Tasks;

using MediatR.CommandQuery.Cosmos.Tests.Constants;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Cosmos.Tests;

public class MockTenantResolver : ITenantResolver<string>
{
    public Task<string> GetTenantId(IPrincipal principal)
    {
        var id = TenantConstants.Test.Id;
        return Task.FromResult(id);
    }
}
