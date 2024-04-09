namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Constants;

public static class PriorityConstants
{
    ///<summary>High Priority</summary>
    public static readonly Guid High = new Guid("dbf0e04f-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>Normal Priority</summary>
    public static readonly Guid Normal = new Guid("dcf0e04f-04fb-e811-aa64-1e872cb6cb93");
    ///<summary>Low Priority</summary>
    public static readonly Guid Low = new Guid("784c7657-04fb-e811-aa64-1e872cb6cb93");
}
