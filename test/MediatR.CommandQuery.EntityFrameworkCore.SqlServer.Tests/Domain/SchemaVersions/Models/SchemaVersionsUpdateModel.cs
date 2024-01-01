using System;
using System.Collections.Generic;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models
{
    public partial class SchemaVersionsUpdateModel
        : EntityUpdateModel
    {
        #region Generated Properties
    public string ScriptName { get; set; }

    public DateTime Applied { get; set; }

    #endregion

    }
}
