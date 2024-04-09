namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Models
{
    public partial class SchemaVersionsCreateModel
        : EntityCreateModel
    {
        #region Generated Properties
    public string ScriptName { get; set; }

    public DateTime Applied { get; set; }

    #endregion

    }
}
