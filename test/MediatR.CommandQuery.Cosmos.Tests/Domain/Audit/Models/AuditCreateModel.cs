using System;

using MediatR.CommandQuery.Cosmos.Tests.Domain;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

public partial class AuditCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    public DateTime Date { get; set; }

    public string UserId { get; set; }

    public string TaskId { get; set; }

    public string Content { get; set; }

    public string Username { get; set; }

    #endregion

}
