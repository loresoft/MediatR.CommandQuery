using System;

using MediatR.CommandQuery.MongoDB.Tests.Data.Entities;

namespace MediatR.CommandQuery.MongoDB.Tests.Constants;

public static class StatusConstants
{
    ///<summary>Not Starated</summary>
    public static readonly Status NotStarted = new() { Id = "607a27ce7d1e32895e494a6a", Name = "Not Started", Description = "Not Started", IsActive = true, DisplayOrder = 1 };
    ///<summary>In Progress</summary>
    public static readonly Status InProgress = new() { Id = "607a27ce7d1e32895e494a66", Name = "In Progress", Description = "In Progress", IsActive = true, DisplayOrder = 2 };
    ///<summary>Completed</summary>
    public static readonly Status Completed = new() { Id = "607a27ce7d1e32895e494a60", Name = "Completed", Description = "Completed", IsActive = true, DisplayOrder = 3 };
    ///<summary>Blocked</summary>
    public static readonly Status Blocked = new() { Id = "607a27d17d1e32895e494a7d", Name = "Blocked", Description = "Blocked", IsActive = true, DisplayOrder = 4 };
    ///<summary>Deferred</summary>
    public static readonly Status Deferred = new() { Id = "607a27d37d1e32895e494a86", Name = "Deferred", Description = "Deferred", IsActive = true, DisplayOrder = 5 };
    ///<summary>Done</summary>
    public static readonly Status Done = new() { Id = "607a27d97d1e32895e494a99", Name = "Done", Description = "Done", IsActive = true, DisplayOrder = 6 };
}
