using System;
using System.Collections.Generic;

namespace Tracker.WebService.Data.Entities;

public partial class UserRole
{
    public UserRole()
    {
        #region Generated Constructor
        #endregion
    }

    #region Generated Properties
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    #endregion

    #region Generated Relationships
    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    #endregion

}
