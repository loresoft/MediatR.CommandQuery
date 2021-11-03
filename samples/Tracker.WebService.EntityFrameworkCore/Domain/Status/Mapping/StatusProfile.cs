using System;
using AutoMapper;
using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<Tracker.WebService.Data.Entities.Status, Tracker.WebService.Domain.Models.StatusReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.StatusCreateModel, Tracker.WebService.Data.Entities.Status>();

            CreateMap<Tracker.WebService.Data.Entities.Status, Tracker.WebService.Domain.Models.StatusUpdateModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.StatusUpdateModel, Tracker.WebService.Data.Entities.Status>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.StatusReadModel, Tracker.WebService.Domain.Models.StatusUpdateModel>();

        }

    }
}
