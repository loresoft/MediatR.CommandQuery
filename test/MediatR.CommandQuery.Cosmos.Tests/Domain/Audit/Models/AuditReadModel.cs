namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

public partial class AuditReadModel
    : EntityReadModel
{
    #region Generated Properties
    public DateTime Date { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TaskId { get; set; }

    public string Content { get; set; }

    public string Username { get; set; }

    #endregion

}
