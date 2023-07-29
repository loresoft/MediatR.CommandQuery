using System;

using MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Mapping;

public partial class TaskExtendedMap
    : IEntityTypeConfiguration<TaskExtended>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TaskExtended> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("TaskExtended", "dbo");

        // key
        builder.HasKey(t => t.TaskId);

        // properties
        builder.Property(t => t.TaskId)
            .IsRequired()
            .HasColumnName("TaskId")
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
        builder.HasOne(t => t.Task)
            .WithOne(t => t.TaskExtended)
            .HasForeignKey<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.TaskExtended>(d => d.TaskId)
            .HasConstraintName("FK_TaskExtended_Task_TaskId");

        #endregion
    }

}
