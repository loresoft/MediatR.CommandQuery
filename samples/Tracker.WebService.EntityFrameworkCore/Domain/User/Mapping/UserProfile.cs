using System;
using AutoMapper;
using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<Tracker.WebService.Data.Entities.User, Tracker.WebService.Domain.Models.UserReadModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.UserCreateModel, Tracker.WebService.Data.Entities.User>();

            CreateMap<Tracker.WebService.Data.Entities.User, Tracker.WebService.Domain.Models.UserUpdateModel>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.ToBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.UserUpdateModel, Tracker.WebService.Data.Entities.User>()
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(s => Convert.FromBase64String(s.RowVersion)));

            CreateMap<Tracker.WebService.Domain.Models.UserReadModel, Tracker.WebService.Domain.Models.UserUpdateModel>();

        }

    }
}
