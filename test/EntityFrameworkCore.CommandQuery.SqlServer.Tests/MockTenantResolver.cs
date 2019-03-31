using System;
using System.Security.Principal;
using EntityFrameworkCore.CommandQuery.Definitions;
using EntityFrameworkCore.CommandQuery.SqlServer.Tests.Constants;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests
{
    public class MockTenantResolver : ITenantResolver<Guid>
    {
        public Guid GetTenantId(IPrincipal principal)
        {
            return TenantConstants.Test;
        }
    }
}