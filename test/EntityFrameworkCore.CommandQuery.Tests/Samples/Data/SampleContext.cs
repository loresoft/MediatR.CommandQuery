using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.CommandQuery.Tests.Samples.Data
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Id);

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(256);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(256);

                entity.Property(e => e.AddressLine3)
                    .HasMaxLength(256);

                entity.Property(e => e.City)
                    .HasMaxLength(150);

                entity.Property(e => e.Created);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100);

                entity.Property(e => e.Latitude);

                entity.Property(e => e.Longitude);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50);

                entity.Property(e => e.StateProvince)
                    .HasMaxLength(150);

                entity.Property(e => e.Updated);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100);
            });

        }
    }
}
