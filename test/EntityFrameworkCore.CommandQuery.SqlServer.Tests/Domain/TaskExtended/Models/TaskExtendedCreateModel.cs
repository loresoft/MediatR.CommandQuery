using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.CommandQuery.SqlServer.Tests.Domain.Models
{
    public partial class TaskExtendedCreateModel
        : EntityCreateModel
    {
        #region Generated Properties
        public Guid TaskId { get; set; }

        public string UserAgent { get; set; }

        public string Browser { get; set; }

        public string OperatingSystem { get; set; }

        #endregion

    }
}
