using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Task.Models;

public partial class TaskReadModel
    : EntityReadModel, IHaveTenant<Guid>, ITrackDeleted
{
    #region Generated Properties
    public Guid StatusId { get; set; }

    public Guid? PriorityId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset? CompleteDate { get; set; }

    public Guid? AssignedId { get; set; }

    public Guid TenantId { get; set; }

    public bool IsDeleted { get; set; }

    #endregion

}
