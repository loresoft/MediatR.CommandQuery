using System.Collections.Generic;

namespace MediatR.CommandQuery.Queries
{
    public class EntityFilter
    {
        public string Name { get; set; }

        public string Operator { get; set; }

        public object Value { get; set; }

        public string Logic { get; set; }

        public IEnumerable<EntityFilter> Filters { get; set; }


        public override int GetHashCode()
        {
            const int m = -1521134295;
            var hashCode = -346447222;

            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Operator);
            hashCode = hashCode * m + EqualityComparer<object>.Default.GetHashCode(Value);
            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Logic);

            if (Filters == null)
                return hashCode;

            foreach (var filter in Filters)
                hashCode = hashCode * m + filter.GetHashCode();

            return hashCode;
        }
    }
}