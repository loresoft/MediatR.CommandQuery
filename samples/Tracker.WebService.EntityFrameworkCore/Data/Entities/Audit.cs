using System;

using MediatR.CommandQuery.Definitions;

namespace Tracker.WebService.Data.Entities;

public partial class Audit : IHaveIdentifier<Guid>, ITrackCreated, ITrackUpdated
{
    public Audit()
    {
        #region Generated Constructor
        #endregion
    }

    #region Generated Properties
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TaskId { get; set; }

    public string Content { get; set; } = null!;

    public string Username { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string? UpdatedBy { get; set; }

    public long RowVersion { get; set; }

    #endregion

    #region Generated Relationships
    #endregion

}
