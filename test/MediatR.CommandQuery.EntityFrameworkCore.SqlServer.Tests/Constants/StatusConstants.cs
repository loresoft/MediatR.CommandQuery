namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;

public static class StatusConstants
{
    ///<summary>Not Starated</summary>
    public static readonly Guid NotStarted = new Guid("ce002cd8-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>In Progress</summary>
    public static readonly Guid InProgress = new Guid("cf002cd8-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>Completed</summary>
    public static readonly Guid Completed = new Guid("d0002cd8-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>Blocked</summary>
    public static readonly Guid Blocked = new Guid("d1002cd8-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>Deferred</summary>
    public static readonly Guid Deferred = new Guid("d2002cd8-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>Done</summary>
    public static readonly Guid Done = new Guid("d3002cd8-04fb-e811-aa64-1e872cb6cb93");
}
