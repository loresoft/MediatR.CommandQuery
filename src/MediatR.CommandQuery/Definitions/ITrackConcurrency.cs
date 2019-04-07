using System;

namespace MediatR.CommandQuery.Definitions
{
    public interface ITrackConcurrency
    {
        string RowVersion { get; set; }
    }
}