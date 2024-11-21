
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Priority.Models;

public partial class PriorityReadModel
    : EntityReadModel, ISupportSearch
{
    #region Generated Properties
    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }
    #endregion

    public static IEnumerable<string> SearchFields() => [nameof(Name), nameof(Description)];

    public static string SortField() => nameof(DisplayOrder);
}
