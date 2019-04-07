using System;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Tenant.Models
{
    public partial class TenantReadModel
        : EntityReadModel
    {
        #region Generated Properties
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        #endregion

    }
}
