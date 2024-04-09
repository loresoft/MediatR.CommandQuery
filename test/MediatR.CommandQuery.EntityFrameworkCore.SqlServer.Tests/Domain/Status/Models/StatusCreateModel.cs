namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Status.Models;

public partial class StatusCreateModel
    : EntityCreateModel
{
    #region Generated Properties
    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    #endregion

}
