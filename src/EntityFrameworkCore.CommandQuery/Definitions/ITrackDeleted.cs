using System;

namespace EntityFrameworkCore.CommandQuery.Definitions
{
    public interface ITrackDeleted
    {
        bool IsDeleted { get; set; }
    }
}
