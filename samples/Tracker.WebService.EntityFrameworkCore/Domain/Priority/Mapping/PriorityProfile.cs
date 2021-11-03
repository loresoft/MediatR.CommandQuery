using System;
using AutoMapper;
using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping
{
    public partial class PriorityProfile
        : AutoMapper.Profile
    {
        public PriorityProfile()
        {
            CreateMap<Tracker.WebService.Data.Entities.Priority, Tracker.WebService.Domain.Models.PriorityReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.PriorityCreateModel, Tracker.WebService.Data.Entities.Priority>();

            CreateMap<Tracker.WebService.Data.Entities.Priority, Tracker.WebService.Domain.Models.PriorityUpdateModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.PriorityUpdateModel, Tracker.WebService.Data.Entities.Priority>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.PriorityReadModel, Tracker.WebService.Domain.Models.PriorityUpdateModel>();

        }

    }
}
