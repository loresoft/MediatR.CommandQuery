using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Mapping;

public partial class UserLoginMap
    : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserLogin> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("UserLogin", "dbo");

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

        builder.Property(t => t.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.UserAgent)
            .HasColumnName("UserAgent")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.Browser)
            .HasColumnName("Browser")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.OperatingSystem)
            .HasColumnName("OperatingSystem")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.DeviceFamily)
            .HasColumnName("DeviceFamily")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.DeviceBrand)
            .HasColumnName("DeviceBrand")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.DeviceModel)
            .HasColumnName("DeviceModel")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

        builder.Property(t => t.IpAddress)
            .HasColumnName("IpAddress")
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50);

        builder.Property(t => t.IsSuccessful)
            .IsRequired()
            .HasColumnName("IsSuccessful")
            .HasColumnType("bit")
            .HasDefaultValue(false);

        builder.Property(t => t.FailureMessage)
            .HasColumnName("FailureMessage")
            .HasColumnType("nvarchar(256)")
            .HasMaxLength(256);

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
        builder.HasOne(t => t.User)
            .WithMany(t => t.UserLogins)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_UserLogin_User_UserId");

        #endregion
    }

}
