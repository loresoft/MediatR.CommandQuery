using MediatR.CommandQuery.Definitions;

using Microsoft.AspNetCore.Identity;

namespace MediatR.CommandQuery.Identity.Tests.Data.Entities;

public class UserClaim : IdentityUserClaim<int>, IHaveIdentifier<int>
{
    public virtual User User { get; set; } = null!;
}
