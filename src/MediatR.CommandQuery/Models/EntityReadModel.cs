using System;
using MediatR.CommandQuery.Definitions;

namespace MediatR.CommandQuery.Models;

public class EntityReadModel<TKey> : EntityIdentifierModel<TKey>, ITrackCreated, ITrackUpdated, ITrackConcurrency
{
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    public string? CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

    public string? UpdatedBy { get; set; }

    public string RowVersion { get; set; } = null!;
}
