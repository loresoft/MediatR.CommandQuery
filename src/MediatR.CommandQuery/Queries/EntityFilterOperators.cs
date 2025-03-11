namespace MediatR.CommandQuery.Queries;

public static class EntityFilterOperators
{
    public const string StartsWith = "StartsWith";
    public const string EndsWith = "EndsWith";
    public const string Contains = "Contains";
    public const string Equal = "eq";
    public const string NotEqual = "neq";
    public const string LessThan = "lt";
    public const string LessThanOrEqual = "lte";
    public const string GreaterThan = "gt";
    public const string GreaterThanOrEqual = "gte";
    public const string In = "in";
    public const string IsNull = "IsNull";
    public const string IsNotNull = "IsNotNull";
    public const string Expression = "Expression";
}
