using System;
using System.Collections.Generic;

using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Data.Entities;

public partial class Status : IHaveIdentifier<Guid>, ITrackCreated, ITrackUpdated
{
    public Status()
    {
        #region Generated Constructor
        Tasks = new HashSet<Task>();
        #endregion
    }

    #region Generated Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string UpdatedBy { get; set; }

    public long RowVersion { get; set; }

    #endregion

    #region Generated Relationships
    public virtual ICollection<Task> Tasks { get; set; }

    #endregion

}
