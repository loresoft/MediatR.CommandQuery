using System;
using System.Collections.Generic;

namespace MediatR.CommandQuery.Queries
{
    public class EntityQuery
    {
        public EntityQuery()
        {
            Page = 1;
            PageSize = 20;

        }

        public EntityQuery(string query, int page, int pageSize, string sort)
        {
            Query = query;
            Page = page;
            PageSize = pageSize;

            var entitySort = EntitySort.Parse(sort);
            if (entitySort == null)
                return;

            Sort = new[] { entitySort };
        }

        public string Query { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<EntitySort> Sort { get; set; }

        public EntityFilter Filter { get; set; }


        public override int GetHashCode()
        {
            const int m = -1521134295;
            var hashCode = -1241527264;

            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Query);
            hashCode = hashCode * m + Page.GetHashCode();
            hashCode = hashCode * m + PageSize.GetHashCode();

            if (Filter != null)
                hashCode = hashCode * m + Filter.GetHashCode();

            if (Sort != null)
                foreach (var s in Sort)
                    hashCode = hashCode * m + s.GetHashCode();

            return hashCode;
        }
    }
}