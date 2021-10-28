using MediatR.CommandQuery.MongoDB.Tests.Domain.Models;

namespace MediatR.CommandQuery.MongoDB.Tests.Domain.Mapping
{
    public partial class UserProfile
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
}
