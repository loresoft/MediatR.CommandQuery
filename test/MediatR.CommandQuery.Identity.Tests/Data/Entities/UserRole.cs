using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Tests.Data.Entities;

public class UserRole : IdentityUserRole<int>
{
    public virtual User User { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
