using System;
using System.Collections.Generic;

namespace MediatR.CommandQuery.Queries
{
    public class EntitySort
    {
        public string Name { get; set; }

        public string Direction { get; set; }


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
            const int m = -1521134295;
            var hashCode = -2111805952;

            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * m + EqualityComparer<string>.Default.GetHashCode(Direction);

            return hashCode;
        }
    }
}