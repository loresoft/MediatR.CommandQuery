using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class AuditProfile
    : AutoMapper.Profile
{
    public AuditProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.Audit, Tracker.WebService.Domain.Models.AuditReadModel>();

        CreateMap<Tracker.WebService.Domain.Models.AuditCreateModel, Tracker.WebService.Data.Entities.Audit>();

        CreateMap<Tracker.WebService.Data.Entities.Audit, Tracker.WebService.Domain.Models.AuditUpdateModel>();

        CreateMap<Tracker.WebService.Domain.Models.AuditUpdateModel, Tracker.WebService.Data.Entities.Audit>();

        CreateMap<Tracker.WebService.Domain.Models.AuditReadModel, Tracker.WebService.Domain.Models.AuditUpdateModel>();

    }

}
