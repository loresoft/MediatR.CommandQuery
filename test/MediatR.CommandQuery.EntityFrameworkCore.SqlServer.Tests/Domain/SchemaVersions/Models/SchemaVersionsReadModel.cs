using System;
using System.Collections.Generic;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models
{
    public partial class SchemaVersionsReadModel
        : EntityReadModel
    {
        #region Generated Properties
    public string ScriptName { get; set; }

    public DateTime Applied { get; set; }

    #endregion

    }
}
