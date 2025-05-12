using MediatR.CommandQuery.Identity.Tests.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.Identity.Tests.Data;

public partial class IdentityContext
    : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new Mapping.UserMap());
        modelBuilder.ApplyConfiguration(new Mapping.RoleMap());
        modelBuilder.ApplyConfiguration(new Mapping.UserClaimMap());
        modelBuilder.ApplyConfiguration(new Mapping.UserLoginMap());
        modelBuilder.ApplyConfiguration(new Mapping.UserTokenMap());
        modelBuilder.ApplyConfiguration(new Mapping.RoleClaimMap());
        modelBuilder.ApplyConfiguration(new Mapping.UserRoleMap());
    }
}
