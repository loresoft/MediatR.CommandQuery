using System;
using System.Collections.Generic;

namespace Tracker.WebService.Domain.Models
{
    public partial class AuditReadModel
        : EntityReadModel
    {
        #region Generated Properties
        public DateTime Date { get; set; }

        public Guid? UserId { get; set; }

        public Guid? TaskId { get; set; }

        public string Content { get; set; } = null!;

        public string Username { get; set; } = null!;

        #endregion

    }
}
