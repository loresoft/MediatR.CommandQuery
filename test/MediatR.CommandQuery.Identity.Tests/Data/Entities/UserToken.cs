using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Tests.Data.Entities;

public class UserToken : IdentityUserToken<int>
{
    public virtual User User { get; set; } = null!;
}
