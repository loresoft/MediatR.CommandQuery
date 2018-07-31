using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.CommandQuery.Queries
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
    }
}