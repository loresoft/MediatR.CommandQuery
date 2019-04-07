using System;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Role.Models
{
    public partial class RoleReadModel
        : EntityReadModel
    {
        #region Generated Properties
        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

    }
}
