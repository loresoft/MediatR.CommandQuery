using System;
using AutoMapper;
using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping
{
    public partial class UserLoginProfile
        : AutoMapper.Profile
    {
        public UserLoginProfile()
        {
            CreateMap<Tracker.WebService.Data.Entities.UserLogin, Tracker.WebService.Domain.Models.UserLoginReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.UserLoginCreateModel, Tracker.WebService.Data.Entities.UserLogin>();

            CreateMap<Tracker.WebService.Data.Entities.UserLogin, Tracker.WebService.Domain.Models.UserLoginUpdateModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.UserLoginUpdateModel, Tracker.WebService.Data.Entities.UserLogin>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.UserLoginReadModel, Tracker.WebService.Domain.Models.UserLoginUpdateModel>();

        }

    }
}
