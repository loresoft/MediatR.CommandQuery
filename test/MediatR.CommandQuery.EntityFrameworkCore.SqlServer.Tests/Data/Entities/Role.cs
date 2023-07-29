using System;
using System.Collections.Generic;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

public partial class Role : IHaveIdentifier<Guid>, ITrackCreated, ITrackUpdated
{
    public Role()
    {
        #region Generated Constructor
        UserRoles = new HashSet<UserRole>();
        #endregion
    }

    #region Generated Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string UpdatedBy { get; set; }

    public Byte[] RowVersion { get; set; }

    #endregion

    #region Generated Relationships
    public virtual ICollection<UserRole> UserRoles { get; set; }

    #endregion

}
