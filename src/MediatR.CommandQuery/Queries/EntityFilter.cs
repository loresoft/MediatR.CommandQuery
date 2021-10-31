using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries
{
    [JsonConverter(typeof(EntityFilterCoverter))]
    public class EntityFilter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("logic")]
        public string Logic { get; set; }

        [JsonPropertyName("filters")]
        public List<EntityFilter> Filters { get; set; }


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
