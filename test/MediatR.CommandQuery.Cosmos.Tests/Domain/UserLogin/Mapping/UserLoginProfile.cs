using MediatR.CommandQuery.Cosmos.Tests.Domain.Models;

namespace MediatR.CommandQuery.Cosmos.Tests.Domain.Mapping
{
    public partial class UserLoginProfile
        : AutoMapper.Profile
    {
        public UserLoginProfile()
        {
            CreateMap<Data.Entities.UserLogin, UserLoginReadModel>();
        }

    }
}
