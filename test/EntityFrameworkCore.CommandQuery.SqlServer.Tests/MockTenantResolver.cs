using System;
using System.Security.Principal;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Constants;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests
{
    public class MockTenantResolver : ITenantResolver<Guid>
    {
        public Task<Guid> GetTenantId(IPrincipal principal)
        {
            return Task.FromResult(TenantConstants.Test);
        }
    }
}