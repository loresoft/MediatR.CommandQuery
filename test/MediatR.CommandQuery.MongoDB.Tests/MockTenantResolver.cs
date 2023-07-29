using System;
using System.Security.Principal;
using System.Threading.Tasks;

using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.MongoDB.Tests.Constants;

namespace MediatR.CommandQuery.MongoDB.Tests;

public class MockTenantResolver : ITenantResolver<string>
{
    public Task<string> GetTenantId(IPrincipal principal)
    {
        var id = TenantConstants.Test.Id;
        return Task.FromResult(id);
    }
}
