using System;
using MediatR.CommandQuery.Definitions;

namespace Tracker.WebService.Domain.Models
{
    public class TaskReadModel
        : EntityReadModel, ITrackDeleted
    {
        public string StatusId { get; set; }

        public string PriorityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? DueDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public string AssignedId { get; set; }

        public bool IsDeleted { get; set; }

    }

}