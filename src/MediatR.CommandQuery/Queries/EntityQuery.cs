using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries
{
    public class EntityQuery : EntitySelect
    {
        public EntityQuery()
        {
            Page = 1;
            PageSize = 20;
        }

        public EntityQuery(string query, int page, int pageSize, string sort)
            : base(query, sort)
        {
            Page = page;
            PageSize = pageSize;
        }

        public EntityQuery(EntityFilter filter, int page = 1, int pageSize = 20)
            : this(filter, (IEnumerable<EntitySort>)null, page, pageSize)
        {
        }

        public EntityQuery(EntityFilter filter, EntitySort sort, int page = 1, int pageSize = 20)
            : this(filter, new[] { sort }, page, pageSize)
        {
        }

        public EntityQuery(EntityFilter filter, IEnumerable<EntitySort> sort, int page = 1, int pageSize = 20)
            : base(filter, sort)
        {
            Page = page;
            PageSize = pageSize;
        }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
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
