using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries
{
    public class EntitySelect
    {
        public EntitySelect()
        {

        }

        public EntitySelect(string query, string sort)
        {
            Query = query;

            var entitySort = EntitySort.Parse(sort);
            if (entitySort == null)
                return;

            Sort = new List<EntitySort> { entitySort };
        }

        public EntitySelect(EntityFilter filter)
            : this(filter, (IEnumerable<EntitySort>)null)
        {
        }

        public EntitySelect(EntityFilter filter, EntitySort sort)
            : this(filter, new[] { sort })
        {

        }

        public EntitySelect(EntityFilter filter, IEnumerable<EntitySort> sort)
        {
            Filter = filter;
            Sort = sort?.ToList();
        }

        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("sort")]
        public List<EntitySort> Sort { get; set; }

        [JsonPropertyName("filter")]
        public EntityFilter Filter { get; set; }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Query);
            
            if (Filter != null)
                hash.Add(Filter.GetHashCode());

            if (Sort != null)
                foreach (var s in Sort)
                    hash.Add(s.GetHashCode());

            return hash.ToHashCode();
        }
    }
}
