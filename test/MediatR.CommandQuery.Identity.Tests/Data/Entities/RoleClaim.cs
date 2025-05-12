using MediatR.CommandQuery.Definitions;

using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Tests.Data.Entities;

public class RoleClaim : IdentityRoleClaim<int>, IHaveIdentifier<int>
{
    public virtual Role Role { get; set; } = null!;
}
