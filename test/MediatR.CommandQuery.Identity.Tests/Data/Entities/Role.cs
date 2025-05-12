using MediatR.CommandQuery.Definitions;

using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Tests.Data.Entities;

public class Role : IdentityRole<int>, IHaveIdentifier<int>
{
    public string? Description { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = null!;

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = null!;
}
