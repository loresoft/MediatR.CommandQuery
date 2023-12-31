using System;

namespace MediatR.CommandQuery.EntityFrameworkCore.SqlServer.Tests.Domain.Audit.Mapping;

public partial class AuditProfile
    : AutoMapper.Profile
{
    public AuditProfile()
    {
        CreateMap<Data.Entities.Audit, Models.AuditReadModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

        CreateMap<Models.AuditReadModel, Models.AuditUpdateModel>();

        CreateMap<Models.AuditCreateModel, Data.Entities.Audit>();

        CreateMap<Data.Entities.Audit, Models.AuditUpdateModel>();

        CreateMap<Models.AuditUpdateModel, Data.Entities.Audit>();
    }

}
