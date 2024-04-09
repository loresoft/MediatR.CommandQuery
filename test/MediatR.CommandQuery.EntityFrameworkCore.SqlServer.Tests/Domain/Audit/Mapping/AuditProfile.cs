namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Mapping;

public partial class AuditProfile
    : AutoMapper.Profile
{
    public AuditProfile()
    {
        CreateMap<Data.Entities.Audit, Models.AuditReadModel>();

        CreateMap<Models.AuditReadModel, Models.AuditUpdateModel>();

        CreateMap<Models.AuditCreateModel, Data.Entities.Audit>();

        CreateMap<Data.Entities.Audit, Models.AuditUpdateModel>();

        CreateMap<Models.AuditUpdateModel, Data.Entities.Audit>();
    }

}
