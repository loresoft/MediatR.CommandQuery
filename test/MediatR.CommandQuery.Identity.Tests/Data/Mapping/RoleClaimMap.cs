using MediatR.CommandQuery.Identity.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatR.CommandQuery.Identity.Tests.Data.Mapping;

public class RoleClaimMap : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(Table.Name, Table.Schema);
    }

    public readonly struct Table
    {
        public const string Schema = "ID";
        public const string Name = nameof(RoleClaim);
    }
}
