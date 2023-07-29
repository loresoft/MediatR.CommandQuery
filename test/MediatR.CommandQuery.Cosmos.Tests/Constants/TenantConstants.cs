using System;

using MediatR.CommandQuery.Cosmos.Tests.Data.Entities;

namespace MediatR.CommandQuery.Cosmos.Tests.Constants;

public static class TenantConstants
{
    ///<summary>Test Tenant</summary>
    public static readonly Tenant Test = new() { Id = "607a27dde412d2a66dd558fb", Name = "Test", Description = "Test Tenant", IsActive = true };
}
