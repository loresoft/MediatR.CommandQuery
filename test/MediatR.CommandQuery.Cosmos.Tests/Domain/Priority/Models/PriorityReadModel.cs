namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

public partial class PriorityReadModel
    : EntityReadModel
{
    #region Generated Properties
    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    #endregion

}
