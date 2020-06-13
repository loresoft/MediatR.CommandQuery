using System.Collections.Generic;

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

            Sort = new[] { entitySort };
        }

        public EntitySelect(EntityFilter filter)
            : this(filter, null)
        {
        }

        public EntitySelect(EntityFilter filter, IEnumerable<EntitySort> sort)
        {
            Filter = filter;
            Sort = sort;
        }

        public string Query { get; set; }

        public IEnumerable<EntitySort> Sort { get; set; }

        public EntityFilter Filter { get; set; }

        public override int GetHashCode()
        {
            const int m = -1521134295;
            var hashCode = -1241527264;

            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Query);

            if (Filter != null)
                hashCode = hashCode * m + Filter.GetHashCode();

            if (Sort != null)
                foreach (var s in Sort)
                    hashCode = hashCode * m + s.GetHashCode();

            return hashCode;
        }
    }
}