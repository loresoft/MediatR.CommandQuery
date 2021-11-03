using System;
using System.Collections.Generic;

namespace Tracker.WebService.Domain.Models
{
    public partial class TaskReadModel
        : EntityReadModel
    {
        #region Generated Properties
        public Guid StatusId { get; set; }

        public Guid? PriorityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? DueDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public Guid? AssignedId { get; set; }

        #endregion

    }
}
