using System;
using System.Security.Principal;
using System.Threading.Tasks;
using MediatR.CommandQuery.Definitions;
using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests
{
    public class MockTenantResolver : ITenantResolver<Guid>
    {
        public Task<Guid> GetTenantId(IPrincipal principal)
        {
            return Task.FromResult(TenantConstants.Test);
        }
    }
}