using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Tracker.WebService.Data.Mapping;

public partial class UserMap
    : IEntityTypeConfiguration<Tracker.WebService.Data.Entities.User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tracker.WebService.Data.Entities.User> builder)
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
            .HasColumnType("bit")
            .HasDefaultValue(false);

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
            .HasColumnType("int")
            .HasDefaultValue(0);

        builder.Property(t => t.LockoutEnabled)
            .IsRequired()
            .HasColumnName("LockoutEnabled")
            .HasColumnType("bit")
            .HasDefaultValue(false);

        builder.Property(t => t.LockoutEnd)
            .HasColumnName("LockoutEnd")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.LastLogin)
            .HasColumnName("LastLogin")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasColumnName("IsDeleted")
            .HasColumnType("bit")
            .HasDefaultValue(false);

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
            .HasConversion<byte[]>()
            .IsRowVersion()
            .IsConcurrencyToken()
            .HasColumnName("RowVersion")
            .HasColumnType("rowversion")
            .ValueGeneratedOnAddOrUpdate();

        // relationships
        #endregion
    }

    #region Generated Constants
    public readonly struct Table
    {
        public const string Schema = "dbo";
        public const string Name = "User";
    }

    public readonly struct Columns
    {
        public const string Id = "Id";
        public const string EmailAddress = "EmailAddress";
        public const string IsEmailAddressConfirmed = "IsEmailAddressConfirmed";
        public const string DisplayName = "DisplayName";
        public const string PasswordHash = "PasswordHash";
        public const string ResetHash = "ResetHash";
        public const string InviteHash = "InviteHash";
        public const string AccessFailedCount = "AccessFailedCount";
        public const string LockoutEnabled = "LockoutEnabled";
        public const string LockoutEnd = "LockoutEnd";
        public const string LastLogin = "LastLogin";
        public const string IsDeleted = "IsDeleted";
        public const string Created = "Created";
        public const string CreatedBy = "CreatedBy";
        public const string Updated = "Updated";
        public const string UpdatedBy = "UpdatedBy";
        public const string RowVersion = "RowVersion";
    }
    #endregion
}
