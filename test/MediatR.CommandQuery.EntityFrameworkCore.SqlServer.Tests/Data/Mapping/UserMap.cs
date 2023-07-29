using System;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Mapping;

public partial class UserMap
    : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("User", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.EmailAddress)
            .IsRequired()
            .HasColumnName("EmailAddress")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.IsEmailAddressConfirmed)
            .IsRequired()
            .HasColumnName("IsEmailAddressConfirmed")
            .HasColumnType("bit");

        builder.Property(t => t.DisplayName)
            .IsRequired()
            .HasColumnName("DisplayName")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.ResetHash)
            .HasColumnName("ResetHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.InviteHash)
            .HasColumnName("InviteHash")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.AccessFailedCount)
            .IsRequired()
            .HasColumnName("AccessFailedCount")
            .HasColumnType("int");

        builder.Property(t => t.LockoutEnabled)
            .IsRequired()
            .HasColumnName("LockoutEnabled")
            .HasColumnType("bit");

        builder.Property(t => t.LockoutEnd)
            .HasColumnName("LockoutEnd")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.LastLogin)
            .HasColumnName("LastLogin")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasColumnName("IsDeleted")
            .HasColumnType("bit");

        builder.Property(t => t.Created)
            .IsRequired()
            .HasColumnName("Created")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.CreatedBy)
            .HasColumnName("CreatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.Updated)
            .IsRequired()
            .HasColumnName("Updated")
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("(sysutcdatetime())");

        builder.Property(t => t.UpdatedBy)
            .HasColumnName("UpdatedBy")
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.RowVersion)
            .IsRequired()
            .IsRowVersion()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .HasMaxLength(8)
            .ValueGeneratedOnAddOrUpdate();

        // relationships
        #endregion
    }

}
