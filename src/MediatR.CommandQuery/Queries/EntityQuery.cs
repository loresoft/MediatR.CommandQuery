using System;

namespace MediatR.CommandQuery.Queries
{
    public class EntityQuery : EntitySelect
    {
        public EntityQuery()
        {
            Page = 1;
            PageSize = 20;
        }

        public EntityQuery(string query, int page, int pageSize, string sort) : base(query, sort)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; set; }

        public int PageSize { get; set; }


        public override int GetHashCode()
        {
            const int m = -1521134295;
            
            int hashCode = base.GetHashCode();
            hashCode = hashCode * m + Page.GetHashCode();
            hashCode = hashCode * m + PageSize.GetHashCode();

            return hashCode;
        }
    }
}