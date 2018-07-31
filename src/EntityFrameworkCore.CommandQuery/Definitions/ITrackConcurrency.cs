using System;

namespace EntityFrameworkCore.CommandQuery.Definitions
{
    public interface ITrackConcurrency
    {
        string RowVersion { get; set; }
    }
}