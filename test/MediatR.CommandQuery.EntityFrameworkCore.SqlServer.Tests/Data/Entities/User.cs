using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

public partial class User : IHaveIdentifier<Guid>, ITrackCreated, ITrackUpdated
{
    public User()
    {
        #region Generated Constructor
        AssignedTasks = new HashSet<Task>();
        UserLogins = new HashSet<UserLogin>();
        UserRoles = new HashSet<UserRole>();
        #endregion
    }

    #region Generated Properties
    public Guid Id { get; set; }

    public string EmailAddress { get; set; }

    public bool IsEmailAddressConfirmed { get; set; }

    public string DisplayName { get; set; }

    public string PasswordHash { get; set; }

    public string ResetHash { get; set; }

    public string InviteHash { get; set; }

    public int AccessFailedCount { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public DateTimeOffset? LastLogin { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string UpdatedBy { get; set; }

    public long RowVersion { get; set; }

    #endregion

    #region Generated Relationships
    public virtual ICollection<Task> AssignedTasks { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }

    #endregion

}
