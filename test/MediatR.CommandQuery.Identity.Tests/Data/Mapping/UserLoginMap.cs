
using MediatR.CommandQuery.Identity.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatR.CommandQuery.Identity.Tests.Data.Mapping;

public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(Table.Name, Table.Schema);
    }

    public readonly struct Table
    {
        public const string Schema = "ID";
        public const string Name = nameof(UserLogin);
    }
}
