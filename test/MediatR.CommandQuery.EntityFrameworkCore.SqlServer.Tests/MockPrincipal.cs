using System.Security.Claims;
using System.Security.Principal;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests;

public static class MockPrincipal
{
    static MockPrincipal()
    {
        Default = CreatePrincipal("william.adama@battlestar.com", "William Adama", UserConstants.WilliamAdama, TenantConstants.Test);
    }


    public static ClaimsPrincipal Default { get; }



    public static ClaimsPrincipal CreatePrincipal(string email, string name, Guid userId, Guid tenantId)
    {
        var claimsIdentity = new ClaimsIdentity("Identity.Application", ClaimTypes.Name, ClaimTypes.Role);
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, name));

        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, email));
        claimsIdentity.AddClaim(new Claim("tenant_id", tenantId.ToString()));

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return claimsPrincipal;
    }
}
