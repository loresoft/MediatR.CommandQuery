using System.Collections.Generic;

namespace EntityFrameworkCore.CommandQuery.Models
{
    public class EntityIdentifiersModel<TKey>
    {
        public IReadOnlyCollection<TKey> Ids { get; set; }
    }
}