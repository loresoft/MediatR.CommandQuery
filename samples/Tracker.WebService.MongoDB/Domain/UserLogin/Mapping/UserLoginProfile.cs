using Tracker.WebService.Domain.Models;

namespace Tracker.WebService.Domain.Mapping;

public class UserLoginProfile
    : AutoMapper.Profile
{
    public UserLoginProfile()
    {
        CreateMap<Data.Entities.UserLogin, UserLoginReadModel>();
    }
}
