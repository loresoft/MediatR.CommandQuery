using Microsoft.EntityFrameworkCore;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Mapping
{
    public partial class SchemaVersionsMap
        : IEntityTypeConfiguration<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.SchemaVersions>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities.SchemaVersions> builder)
        {
            #region Generated Configure
        // table
        builder.ToTable("SchemaVersions", "dbo");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.ScriptName)
            .IsRequired()
            .HasColumnName("ScriptName")
            .HasColumnType("nvarchar(255)")
            .HasMaxLength(255);

        builder.Property(t => t.Applied)
            .IsRequired()
            .HasColumnName("Applied")
            .HasColumnType("datetime");

        // relationships
        #endregion
        }

        #region Generated Constants
    public readonly struct Table
    {
        public const string Schema = "dbo";
        public const string Name = "SchemaVersions";
    }

    public readonly struct Columns
    {
        public const string Id = "Id";
        public const string ScriptName = "ScriptName";
        public const string Applied = "Applied";
    }
    #endregion
    }
}
