using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public class UserProfile
    : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<Data.Entities.User, UserReadModel>();
        CreateMap<UserCreateModel, Data.Entities.User>();
        CreateMap<Data.Entities.User, UserUpdateModel>();
        CreateMap<UserUpdateModel, Data.Entities.User>();
    }
}
