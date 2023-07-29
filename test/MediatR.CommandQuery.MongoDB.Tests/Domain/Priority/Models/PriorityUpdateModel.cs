namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

public partial class PriorityUpdateModel
    : EntityUpdateModel
{
    #region Generated Properties
    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    #endregion

}
