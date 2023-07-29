using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class RoleProfile
    : AutoMapper.Profile
{
    public RoleProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.Role, Tracker.WebService.Domain.Models.RoleReadModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

        CreateMap<Tracker.WebService.Domain.Models.RoleCreateModel, Tracker.WebService.Data.Entities.Role>();

        CreateMap<Tracker.WebService.Data.Entities.Role, Tracker.WebService.Domain.Models.RoleUpdateModel>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

        CreateMap<Tracker.WebService.Domain.Models.RoleUpdateModel, Tracker.WebService.Data.Entities.Role>()
            .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));

        CreateMap<Tracker.WebService.Domain.Models.RoleReadModel, Tracker.WebService.Domain.Models.RoleUpdateModel>();

    }

}
