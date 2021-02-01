using System;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models
{
    public class EntityCreateModel<TKey> : EntityIdentifierModel<TKey>, ITrackCreated, ITrackUpdated
    {
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

        public string UpdatedBy { get; set; }
    }
}