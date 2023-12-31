using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class UserLoginProfile
    : AutoMapper.Profile
{
    public UserLoginProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.UserLogin, Tracker.WebService.Domain.Models.UserLoginReadModel>();

        CreateMap<Tracker.WebService.Domain.Models.UserLoginCreateModel, Tracker.WebService.Data.Entities.UserLogin>();

        CreateMap<Tracker.WebService.Data.Entities.UserLogin, Tracker.WebService.Domain.Models.UserLoginUpdateModel>();

        CreateMap<Tracker.WebService.Domain.Models.UserLoginUpdateModel, Tracker.WebService.Data.Entities.UserLogin>();

        CreateMap<Tracker.WebService.Domain.Models.UserLoginReadModel, Tracker.WebService.Domain.Models.UserLoginUpdateModel>();

    }

}
