using System;

namespace Tracker.WebService.Domain.Models
{
    public class AuditCreateModel
        : EntityCreateModel
    {
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string TaskId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}
