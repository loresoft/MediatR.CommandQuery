using System;
using AutoMapper;
using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping
{
    public partial class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<Tracker.WebService.Data.Entities.Audit, Tracker.WebService.Domain.Models.AuditReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.AuditCreateModel, Tracker.WebService.Data.Entities.Audit>();

            CreateMap<Tracker.WebService.Data.Entities.Audit, Tracker.WebService.Domain.Models.AuditUpdateModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.AuditUpdateModel, Tracker.WebService.Data.Entities.Audit>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.AuditReadModel, Tracker.WebService.Domain.Models.AuditUpdateModel>();

        }

    }
}
