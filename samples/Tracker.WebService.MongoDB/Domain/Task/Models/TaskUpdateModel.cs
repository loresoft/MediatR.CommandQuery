using System;
using MediatR.CommandQuery.Definitions;

namespace Tracker.WebService.Domain.Models
{
    public class TaskUpdateModel
        : EntityUpdateModel, ITrackDeleted
    {
        public string StatusId { get; set; } = null!;

        public string? PriorityId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? DueDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public string? AssignedId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
