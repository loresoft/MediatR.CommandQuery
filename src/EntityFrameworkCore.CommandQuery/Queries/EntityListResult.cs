using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    public class EntityListResult<TReadModel>
    {
        public long Total { get; set; }

        public IReadOnlyCollection<TReadModel> Data { get; set; }
    }
}