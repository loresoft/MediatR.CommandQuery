using System;

namespace EntityFrameworkCore.CommandQuery.Queries
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
    }
}