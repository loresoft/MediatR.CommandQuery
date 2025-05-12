using MediatR.CommandQuery.Definitions;

using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Tests.Data.Entities;

public class User : IdentityUser<int>, IHaveIdentifier<int>
{
    public string? DisplayName { get; set; }


    public virtual ICollection<UserClaim> Claims { get; set; } = null!;

    public virtual ICollection<UserLogin> Logins { get; set; } = null!;

    public virtual ICollection<UserToken> Tokens { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = null!;
}
