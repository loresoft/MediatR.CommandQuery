using System;
using System.Text.Json.Serialization;

namespace MediatR.CommandQuery.Queries
{
    public class EntitySort
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; } = EntitySortDirections.Ascending;


        public static EntitySort Parse(string sortString)
        {
            if (string.IsNullOrEmpty(sortString))
                return null;

            var parts = sortString.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return null;

            var sort = new EntitySort();
            sort.Name = parts[0]?.Trim();

            if (parts.Length >= 2)
                sort.Direction = parts[1]?.Trim();

            return sort;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Direction);
        }

        public override string ToString()
        {
            return $"{Name}:{Direction ?? EntitySortDirections.Ascending}";
        }
    }
}
