using System;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Models
{
    public partial class AuditUpdateModel
        : EntityUpdateModel
    {
        #region Generated Properties
        public DateTime Date { get; set; }

        public Guid? UserId { get; set; }

        public Guid? TaskId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        #endregion

    }
}
