using System;
using EntityFrameworkCore.CommandQuery.Definitions;

namespace EntityFrameworkCore.CommandQuery.Models
{
    public abstract class EntityUpdateModel : ITrackUpdated,  ITrackConcurrency
    {
        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

        public string UpdatedBy { get; set; }

        public string RowVersion { get; set; }
    }
}