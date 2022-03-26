using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tracker.WebService.Data
{
    public partial class TrackerServiceContext : DbContext
    {
        public TrackerServiceContext(DbContextOptions<TrackerServiceContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<Tracker.WebService.Data.Entities.Audit> Audits { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.Priority> Priorities { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.Role> Roles { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.Status> Statuses { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.Task> Tasks { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.UserLogin> UserLogins { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.UserRole> UserRoles { get; set; } = null!;

        public virtual DbSet<Tracker.WebService.Data.Entities.User> Users { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.AuditMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.PriorityMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.RoleMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.TaskMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.UserLoginMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Tracker.WebService.Data.Mapping.UserRoleMap());
            #endregion
        }
    }
}
