using System.Security.Claims;
using System.Security.Principal;

using MediatR.CommandQuery.MongoDB.Tests.Constants;

namespace MediatR.CommandQuery.MongoDB.Tests;

public static class MockPrincipal
{
    static MockPrincipal()
    {
        Default = CreatePrincipal("william.adama@battlestar.com", "William Adama", UserConstants.WilliamAdama.Id, TenantConstants.Test.Id);
    }


    public static ClaimsPrincipal Default { get; }



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
