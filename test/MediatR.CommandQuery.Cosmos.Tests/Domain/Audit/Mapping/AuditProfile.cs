using System;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Mapping
{
    public partial class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<Cosmos.Tests.Data.Entities.Audit, Models.AuditReadModel>();

            CreateMap<Models.AuditReadModel, Models.AuditUpdateModel>();

            CreateMap<Models.AuditCreateModel, Cosmos.Tests.Data.Entities.Audit>();

            CreateMap<Cosmos.Tests.Data.Entities.Audit, Models.AuditUpdateModel>();

            CreateMap<Models.AuditUpdateModel, Cosmos.Tests.Data.Entities.Audit>();
        }

    }
}
