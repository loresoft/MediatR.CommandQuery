using System;

namespace EntityFrameworkCore.CommandQuery.Queries
{
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
    }
}