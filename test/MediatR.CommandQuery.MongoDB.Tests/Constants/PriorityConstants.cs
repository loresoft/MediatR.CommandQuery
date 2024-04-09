using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

namespace MediatR.CommandQuery.MongoDB.Tests.Constants;

public static class PriorityConstants
{
    ///<summary>High Priority</summary>
    public static readonly Priority High = new() { Id = "607a27d37d1e32895e494a80", Name = "High", Description = "High Priority", IsActive = true, DisplayOrder = 1 };
    ///<summary>Normal Priority</summary>
    public static readonly Priority Normal = new() { Id = "607a27d57d1e32895e494a8b", Name = "Normal", Description = "Normal Priority", IsActive = true, DisplayOrder = 2 };
    ///<summary>Low Priority</summary>
    public static readonly Priority Low = new() { Id = "607a27d77d1e32895e494a95", Name = "Low", Description = "Low Priority", IsActive = true, DisplayOrder = 3 };
}
