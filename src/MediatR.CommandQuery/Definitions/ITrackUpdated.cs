using System;

namespace MediatR.CommandQuery.Definitions;

public interface ITrackUpdated
{
    DateTimeOffset Updated { get; set; }
    string? UpdatedBy { get; set; }
}
