using System;

using AutoMapper;

using Tracker.WebService.Data.Entities;
using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public partial class UserProfile
    : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<Tracker.WebService.Data.Entities.User, Tracker.WebService.Domain.Models.UserReadModel>();

        CreateMap<Tracker.WebService.Domain.Models.UserCreateModel, Tracker.WebService.Data.Entities.User>();

        CreateMap<Tracker.WebService.Data.Entities.User, Tracker.WebService.Domain.Models.UserUpdateModel>();

        CreateMap<Tracker.WebService.Domain.Models.UserUpdateModel, Tracker.WebService.Data.Entities.User>();

        CreateMap<Tracker.WebService.Domain.Models.UserReadModel, Tracker.WebService.Domain.Models.UserUpdateModel>();

    }

}
