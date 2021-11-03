using System;

namespace Tracker.WebService.Domain.Models
{
    public class AuditReadModel
        : EntityReadModel
    {
        public DateTime Date { get; set; }

        public Guid? UserId { get; set; }

        public Guid? TaskId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}
