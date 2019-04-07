using System;

namespace MediatR.CommandQuery.Definitions
{
    public interface ITrackDeleted
    {
        bool IsDeleted { get; set; }
    }
}
