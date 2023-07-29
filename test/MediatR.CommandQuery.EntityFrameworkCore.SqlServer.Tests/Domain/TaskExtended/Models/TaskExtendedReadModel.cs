using System;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.TaskExtended.Models;

public partial class TaskExtendedReadModel
    : EntityReadModel
{
    #region Generated Properties
    public Guid TaskId { get; set; }

    public string UserAgent { get; set; }

    public string Browser { get; set; }

    public string OperatingSystem { get; set; }

    #endregion

}
