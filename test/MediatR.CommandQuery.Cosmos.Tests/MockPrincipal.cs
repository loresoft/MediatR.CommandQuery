using System;
using System.Security.Claims;
using System.Security.Principal;

using MediatR.CommandQuery.Cosmos.Tests.Constants;

namespace MediatR.CommandQuery.Cosmos.Tests;

public class MockPrincipal
{
    static MockPrincipal()
    {
        Default = CreatePrincipal("william.adama@battlestar.com", "William Adama", UserConstants.WilliamAdama.Id, TenantConstants.Test.Id);
    }


    public static IPrincipal Default { get; }



    public static ClaimsPrincipal CreatePrincipal(string email, string name, string userId, string tenantId)
    {
        var claimsIdentity = new ClaimsIdentity("Identity.Application", ClaimTypes.Name, ClaimTypes.Role);
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, name));

        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, email));
        claimsIdentity.AddClaim(new Claim("tenant_id", tenantId));

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return claimsPrincipal;
    }
}
