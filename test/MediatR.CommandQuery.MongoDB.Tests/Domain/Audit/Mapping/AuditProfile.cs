using System;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Mapping;

public partial class AuditProfile
    : AutoMapper.Profile
{
    public AuditProfile()
    {
        CreateMap<MongoDB.Tests.Data.Entities.Audit, Models.AuditReadModel>();

        CreateMap<Models.AuditReadModel, Models.AuditUpdateModel>();

        CreateMap<Models.AuditCreateModel, MongoDB.Tests.Data.Entities.Audit>();

        CreateMap<MongoDB.Tests.Data.Entities.Audit, Models.AuditUpdateModel>();

        CreateMap<Models.AuditUpdateModel, MongoDB.Tests.Data.Entities.Audit>();
    }

}
