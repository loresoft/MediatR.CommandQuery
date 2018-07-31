using System;

namespace EntityFrameworkCore.CommandQuery.Definitions
{
    public interface ITrackCreated
    {
        DateTimeOffset Created { get; set; }
        string CreatedBy { get; set; }
    }
}