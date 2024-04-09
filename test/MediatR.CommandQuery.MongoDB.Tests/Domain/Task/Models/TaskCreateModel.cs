using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

public partial class TaskCreateModel
    : EntityCreateModel, IHaveTenant<string>, ITrackDeleted
{
    #region Generated Properties
    public string StatusId { get; set; }

    public string PriorityId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset? CompleteDate { get; set; }

    public string AssignedId { get; set; }

    public string TenantId { get; set; }

    public bool IsDeleted { get; set; }

    #endregion

}
