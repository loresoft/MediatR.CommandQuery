using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Mapping;

public partial class TaskMap
    : IEntityTypeConfiguration<Entities.Task>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Task> builder)
    {
        #region Generated Configure
        // table
        builder.ToTable("Task", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("(newsequentialid())");

        builder.Property(t => t.StatusId)
            .IsRequired()
            .HasColumnName("StatusId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.PriorityId)
            .HasColumnName("PriorityId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255);

        builder.Property(t => t.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.DueDate)
            .HasColumnName("DueDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.CompleteDate)
            .HasColumnName("CompleteDate")
            .HasColumnType("datetimeoffset");

        builder.Property(t => t.AssignedId)
            .HasColumnName("AssignedId")
            .HasColumnType("uniqueidentifier");

        builder.Property(t => t.TenantId)
            .IsRequired()
            .HasColumnName("TenantId")
            .HasColumnType("uniqueidentifier");

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
        builder.HasOne(t => t.Priority)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.PriorityId)
            .HasConstraintName("FK_Task_Priority_PriorityId");

        builder.HasOne(t => t.Status)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.StatusId)
            .HasConstraintName("FK_Task_Status_StatusId");

        builder.HasOne(t => t.Tenant)
            .WithMany(t => t.Tasks)
            .HasForeignKey(d => d.TenantId)
            .HasConstraintName("FK_Task_Tenant_TenantId");

        builder.HasOne(t => t.AssignedUser)
            .WithMany(t => t.AssignedTasks)
            .HasForeignKey(d => d.AssignedId)
            .HasConstraintName("FK_Task_User_AssignedId");

        #endregion
    }

}
