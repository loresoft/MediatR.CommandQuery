using System;

namespace MediatR.CommandQuery.Definitions;

public interface ITrackCreated
{
    DateTimeOffset Created { get; set; }
    string? CreatedBy { get; set; }
}
